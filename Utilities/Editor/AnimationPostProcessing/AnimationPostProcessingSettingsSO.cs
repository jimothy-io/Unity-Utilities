using UnityEditor;
using UnityEngine;

namespace Jimothy.Utilities.AnimationPostProcessing
{
    [CreateAssetMenu(fileName = "AnimationSettings", menuName = "AnimationPostProcessor/Settings", order = 1)]
    public class AnimationPostProcessingSettingsSO : ScriptableObject {
        public bool Enabled = false;
        public string TargetFolder = "Assets/_Project/Animations";
        public Avatar ReferenceAvatar;
        public GameObject ReferenceFBX;
    
        public bool EnableTranslationDoF = true;
        public ModelImporterAnimationType AnimationType = ModelImporterAnimationType.Human;
        public bool LoopTime = true;
        public bool RenameClips = true;
        public bool ForceEditorApply = true;
        public bool ExtractTextures = false;
    }
}