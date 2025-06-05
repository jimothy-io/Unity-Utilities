using System.Reflection;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Jimothy.Utilities.Editor.Animator
{
    [CustomPreview(typeof(AnimatorState))]
    public class AnimatorStateObjectPreview : ObjectPreview
    {
        private static FieldInfo _cachedAvatarPreviewField;
        private static FieldInfo _cachedTimeControlField;
        private static FieldInfo _cachedStopTimeField;

        private UnityEditor.Editor _preview;
        private int _animationClipId;

        public override void Initialize(Object[] targets)
        {
            base.Initialize(targets);

            if (targets.Length > 1 || Application.isPlaying) return;

            SourceAnimationClipEditorFields();

            AnimationClip clip = GetAnimationClip(target as AnimatorState);
            if (clip != null)
            {
                AnimationClipEditor editor = UnityEditor.Editor.CreateEditor(clip) as AnimationClipEditor;
                _preview = UnityEditor.Editor.CreateEditor(clip);
                _animationClipId = clip.GetInstanceID();
            }
        }

        private void SourceAnimationClipEditorFields()
        {
            if (_cachedAvatarPreviewField != null) return;

            // Reflection can get messy between Unity versions.
            // If you run into issues, you may need to adjust the field names.
            _cachedAvatarPreviewField = System.Type.GetType("UnityEditor.AnimationClipEditor, UnityEditor")
                .GetField("m_AvatarPreview", BindingFlags.NonPublic | BindingFlags.Instance);
            _cachedTimeControlField = System.Type.GetType("UnityEditor.AvatarPreview, UnityEditor")
                .GetField("timeControl", BindingFlags.Public | BindingFlags.Instance);
            _cachedStopTimeField = System.Type.GetType("UnityEditor.TimeControl, UnityEditor")
                .GetField("stopTime", BindingFlags.Public | BindingFlags.Instance);
        }

        public override bool HasPreviewGUI()
        {
            return _preview?.HasPreviewGUI() ?? false;
        }

        public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
        {
            base.OnInteractivePreviewGUI(r, background);

            AnimationClip currentClip = GetAnimationClip(target as AnimatorState);
            if (currentClip != null && currentClip.GetInstanceID() != _animationClipId)
            {
                CleanupPreviewEditor();
                _preview = UnityEditor.Editor.CreateEditor(currentClip);
                _animationClipId = currentClip.GetInstanceID();
                return;
            }

            if (_preview != null)
            {
                UpdateAnimationClipEditor(_preview, currentClip);
                _preview.OnInteractivePreviewGUI(r, background);
            }
        }

        private void UpdateAnimationClipEditor(UnityEditor.Editor editor, AnimationClip currentClip)
        {
            if (_cachedAvatarPreviewField == null || _cachedTimeControlField == null || _cachedStopTimeField == null)
                return;
            
            var avatarPreview = _cachedAvatarPreviewField.GetValue(editor);
            var timeControl = _cachedTimeControlField.GetValue(avatarPreview);
            
            _cachedStopTimeField.SetValue(timeControl, currentClip.length);
        }

        public override void Cleanup()
        {
            base.Cleanup();

            CleanupPreviewEditor();
        }

        private AnimationClip GetAnimationClip(AnimatorState state)
        {
            return state?.motion as AnimationClip;
        }

        private void CleanupPreviewEditor()
        {
            if (_preview != null)
            {
                Object.DestroyImmediate(_preview);
                _preview = null;
                _animationClipId = 0;
            }
        }
    }
}