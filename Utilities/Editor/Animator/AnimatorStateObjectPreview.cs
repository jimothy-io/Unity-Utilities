using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Jimothy.Utilities.Editor.Animator
{
    [CustomPreview(typeof(AnimatorState))]
    public class AnimatorStateObjectPreview : ObjectPreview
    {
        private UnityEditor.Editor _preview;
        private int _animationClipId;

        public override void Initialize(Object[] targets)
        {
            base.Initialize(targets);

            if (targets.Length > 1 || Application.isPlaying) return;

            AnimationClip clip = GetAnimationClip(target as AnimatorState);
            if (clip != null)
            {
                _preview = UnityEditor.Editor.CreateEditor(clip);
                _animationClipId = clip.GetInstanceID();
            }
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
                _preview.OnInteractivePreviewGUI(r, background);
            }
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