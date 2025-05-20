using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Jimothy.Utilities.Editor.Animator
{
    [CustomPreview(typeof(AnimatorState))]
    public class AnimatorStateObjectPreview : ObjectPreview
    {
        private UnityEditor.Editor _preview;

        public override void Initialize(Object[] targets)
        {
            base.Initialize(targets);

            if (targets.Length > 1 || Application.isPlaying) return;

            AnimatorState state = (AnimatorState)target;
            if (state.motion && state.motion is AnimationClip animationClip)
            {
                _preview = UnityEditor.Editor.CreateEditor(animationClip);
            }
        }

        public override bool HasPreviewGUI()
        {
            return _preview?.HasPreviewGUI() ?? false;
        }

        public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
        {
            base.OnInteractivePreviewGUI(r, background);

            if (_preview != null)
            {
                _preview.OnInteractivePreviewGUI(r, background);
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();

            if (_preview != null)
            {
                Object.DestroyImmediate(_preview);
                _preview = null;
            }
        }
    }
}