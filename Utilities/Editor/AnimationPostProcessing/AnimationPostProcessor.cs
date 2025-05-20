using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Jimothy.Utilities.Editor.AnimationPostProcessing
{
    public class AnimationPostProcessor : AssetPostprocessor
    {
        private static AnimationPostProcessingSettingsSO _settings;
        private static Avatar _referenceAvatar;
        private static GameObject _referenceFBX;
        private static ModelImporter _referenceImporter;
        private static bool _settingsLoaded;


        private void OnPreprocessModel()
        {
            LoadSettings();
            if (!_settingsLoaded || !_settings.Enabled) return;

            // Check if asset is in the specified folder.
            var importer = assetImporter as ModelImporter;
            AssetDatabase.ImportAsset(importer?.assetPath);

            // Extract materials and textures.
            if (_settings.ExtractTextures)
            {
                importer.ExtractTextures(Path.GetDirectoryName(importer.assetPath));
                importer.materialLocation = ModelImporterMaterialLocation.External;
            }

            // Extract avatar from the reference FBX if not already specified.
            if (_referenceAvatar == null)
            {
                _referenceAvatar = _referenceImporter.sourceAvatar;
            }

            // Set the avatar and the rig type of the imported model.
            importer.sourceAvatar = _referenceAvatar;
            importer.animationType = _settings.AnimationType;

            // Set the animation to Generic if the avatar is invalid.
            if (_referenceAvatar == null || !_referenceAvatar.isValid)
            {
                importer.animationType = ModelImporterAnimationType.Generic;
            }

            // Use serialization to set the avatar correctly.
            SerializedObject serializedObject =
                new SerializedObject((UnityEngine.Object)importer.sourceAvatar);
            using SerializedObject sourceObject =
                new SerializedObject((UnityEngine.Object)_referenceAvatar);
            CopyHumanDescription(sourceObject, serializedObject);
            serializedObject.ApplyModifiedProperties();

            importer.sourceAvatar = (Avatar)serializedObject.targetObject;
            serializedObject.Dispose();

            // Translation Degrees of Freedom.
            if (_settings.EnableTranslationDoF)
            {
                var importerHumanDescription = importer.humanDescription;
                importerHumanDescription.hasTranslationDoF = true;
                importer.humanDescription = importerHumanDescription;
            }

            // Use reflection to instantiate an Editor and call the Apply method as if the Apply button was pressed.
            if (_settings.ForceEditorApply)
            {
                var editorType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ModelImporterEditor");
                var nonPublic = BindingFlags.NonPublic | BindingFlags.Instance;
                var editor = UnityEditor.Editor.CreateEditor(importer, editorType);
                editorType.GetMethod("Apply", nonPublic).Invoke(editor, null);
                UnityEngine.Object.DestroyImmediate(editor);
            }
        }

        private void CopyHumanDescription(SerializedObject sourceObject,
            SerializedObject serializedObject)
        {
            serializedObject.CopyFromSerializedProperty(
                sourceObject.FindProperty("m_HumanDescription"));
        }

        private void OnPreprocessAnimation()
        {
            LoadSettings();
            if (!_settingsLoaded || !_settings.Enabled) return;

            ModelImporter importer = assetImporter as ModelImporter;

            // Check if asset is in the specified folder.
            if (!importer.assetPath.StartsWith(_settings.TargetFolder)) return;

            ModelImporter modelImporter = CopyModelImporterSettings(importer);

            AssetDatabase.ImportAsset(modelImporter.assetPath, ImportAssetOptions.ForceUpdate);
        }

        private ModelImporter CopyModelImporterSettings(ModelImporter modelImporter)
        {
            // Model
            modelImporter.globalScale = _referenceImporter.globalScale;
            modelImporter.useFileScale = _referenceImporter.useFileScale;
            modelImporter.meshCompression = _referenceImporter.meshCompression;
            modelImporter.isReadable = _referenceImporter.isReadable;
            modelImporter.optimizeMeshPolygons = _referenceImporter.optimizeMeshPolygons;
            modelImporter.optimizeMeshVertices = _referenceImporter.optimizeMeshVertices;
            modelImporter.importBlendShapes = _referenceImporter.importBlendShapes;
            modelImporter.keepQuads = _referenceImporter.keepQuads;
            modelImporter.indexFormat = _referenceImporter.indexFormat;
            modelImporter.weldVertices = _referenceImporter.weldVertices;
            modelImporter.importVisibility = _referenceImporter.importVisibility;
            modelImporter.importCameras = _referenceImporter.importCameras;
            modelImporter.importLights = _referenceImporter.importLights;
            modelImporter.preserveHierarchy = _referenceImporter.preserveHierarchy;
            modelImporter.swapUVChannels = _referenceImporter.swapUVChannels;
            modelImporter.generateSecondaryUV = _referenceImporter.generateSecondaryUV;
            modelImporter.importNormals = _referenceImporter.importNormals;
            modelImporter.normalCalculationMode = _referenceImporter.normalCalculationMode;
            modelImporter.normalSmoothingAngle = _referenceImporter.normalSmoothingAngle;
            modelImporter.importTangents = _referenceImporter.importTangents;

            // Rig
            modelImporter.animationType = _referenceImporter.animationType;
            modelImporter.optimizeGameObjects = _referenceImporter.optimizeGameObjects;

            // Materials
            modelImporter.materialImportMode = _referenceImporter.materialImportMode;
            modelImporter.materialLocation = _referenceImporter.materialLocation;
            modelImporter.materialName = _referenceImporter.materialName;

            // Get the filename of the FBX in case we want to use it for the animation name.
            var fileName = Path.GetFileNameWithoutExtension(modelImporter.assetPath);

            // Animations
            if (_referenceImporter.clipAnimations.Length == 0) return modelImporter;

            // Copy the first reference clip settings to all imported clips.
            var referenceClip = _referenceImporter.clipAnimations[0];

            var referenceClipAnimations = _referenceImporter.defaultClipAnimations;

            var defaultClipAnimations = modelImporter.defaultClipAnimations;
            foreach (var clipAnimation in defaultClipAnimations)
            {
                clipAnimation.hasAdditiveReferencePose = referenceClip.hasAdditiveReferencePose;
                if (referenceClip.hasAdditiveReferencePose)
                {
                    clipAnimation.additiveReferencePoseFrame =
                        referenceClip.additiveReferencePoseFrame;
                }

                // Rename if needed.
                if (_settings.RenameClips)
                {
                    if (referenceClipAnimations.Length == 1)
                    {
                        clipAnimation.name = fileName;
                    }
                    else
                    {
                        clipAnimation.name = fileName + "" + clipAnimation.name;
                    }
                }

                // Set loop time.
                clipAnimation.loopTime = _settings.LoopTime;

                clipAnimation.maskType = referenceClip.maskType;
                clipAnimation.maskSource = referenceClip.maskSource;

                clipAnimation.keepOriginalOrientation = referenceClip.keepOriginalOrientation;
                clipAnimation.keepOriginalPositionXZ = referenceClip.keepOriginalPositionXZ;
                clipAnimation.keepOriginalPositionY = referenceClip.keepOriginalPositionY;

                clipAnimation.lockRootRotation = referenceClip.lockRootRotation;
                clipAnimation.lockRootPositionXZ = referenceClip.lockRootPositionXZ;
                clipAnimation.lockRootHeightY = referenceClip.lockRootHeightY;

                clipAnimation.mirror = referenceClip.mirror;
                clipAnimation.wrapMode = referenceClip.wrapMode;
            }
            
            modelImporter.clipAnimations = defaultClipAnimations;

            return modelImporter;
        }

        private static void LoadSettings()
        {
            var guids = AssetDatabase.FindAssets("t:AnimationPostProcessingSettingsSO");
            if (guids.Length > 0)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                _settings = AssetDatabase.LoadAssetAtPath<AnimationPostProcessingSettingsSO>(path);

                _referenceAvatar = _settings.ReferenceAvatar;
                _referenceFBX = _settings.ReferenceFBX;
                _referenceImporter =
                    AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(_referenceFBX)) as
                        ModelImporter;

                _settingsLoaded = true;
            }
            else
            {
                _settingsLoaded = false;
            }
        }
    }
}