using System;
using System.Collections;
using Jimothy.Utilities.Extensions;
using UnityEngine;

namespace Jimothy.Utilities.Tools
{
    public class SquashAndStretcher : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private Transform _target;
        [SerializeField] private SquashStretchAxes _axes = SquashStretchAxes.All;
        [SerializeField] private bool _allowOverride = true;
        [SerializeField] private bool _playOnStart = false;
        [SerializeField] private bool _playAlways = true;
        [SerializeField, Range(0, 1f)] private float _chanceToPlay = 1f;

        [Header("Animation Settings")]
        [SerializeField] private float _animationDuration = 0.3f;
        [SerializeField] private float _initialScale = 1f;
        [SerializeField] private float _maxScale = 1.5f;
        [SerializeField] private AnimationCurve _animationCurve = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.25f, 1f),
            new Keyframe(1f, 0f)
        );

        private Coroutine _coroutine;
        private Vector3 _initialScaleVector;
        private static event Action SquashAndStretchAllTriggered;

        private bool AffectX => (_axes & SquashStretchAxes.X) != 0;
        private bool AffectY => (_axes & SquashStretchAxes.Y) != 0;
        private bool AffectZ => (_axes & SquashStretchAxes.Z) != 0;

        [Flags]
        public enum SquashStretchAxes
        {
            None = 0,
            X = 1 << 0,
            Y = 1 << 1,
            Z = 1 << 2,
            All = X | Y | Z
        }

        private void Awake()
        {
            if (_target == null)
            {
                _target = transform;
            }

            _initialScaleVector = _target.localScale;

            if (_animationDuration <= 0f)
            {
                Debug.LogWarning("Squash and stretch duration must be greater than 0.");
                _animationDuration = 0.1f;
            }
        }

        private void Start()
        {
            if (_playOnStart) Play();
        }

        [ContextMenu("Play")]
        public void Play()
        {
            if (_axes == SquashStretchAxes.None) return;

            if (_coroutine != null)
            {
                if (_allowOverride) Reset();
                else return;
            }

            if (_playAlways || UnityEngine.Random.value <= _chanceToPlay)
            {
                _coroutine = StartCoroutine(SquashAndStretchRoutine());
            }
        }

        private IEnumerator SquashAndStretchRoutine()
        {
            var elapsedTime = 0f;
            while (elapsedTime < _animationDuration)
            {
                elapsedTime += Time.deltaTime;

                float curvePosition = elapsedTime / _animationDuration;

                float curveValue = _animationCurve.Evaluate(curvePosition);
                float remappedValue = _initialScale + curveValue * (_maxScale - _initialScale);

                Vector3 modifiedScale;
                modifiedScale.x =
                    AffectX ? _initialScaleVector.x * remappedValue : _initialScaleVector.x / remappedValue;
                modifiedScale.y =
                    AffectY ? _initialScaleVector.y * remappedValue : _initialScaleVector.y / remappedValue;
                modifiedScale.z =
                    AffectZ ? _initialScaleVector.z * remappedValue : _initialScaleVector.z / remappedValue;

                _target.localScale = modifiedScale;

                yield return null;
            }

            _target.localScale = _initialScaleVector;
            _coroutine = null;
        }

        [ContextMenu("Play All")]
        public void SquashAndStretchAll()
        {
            SquashAndStretchAllTriggered?.Invoke();
        }

        public void Reset()
        {
            this.StopAndNullifyCoroutine(ref _coroutine);
            _target.localScale = _initialScaleVector;
        }

        private void OnEnable()
        {
            SquashAndStretchAllTriggered += Play;
        }

        private void OnDisable()
        {
            SquashAndStretchAllTriggered -= Play;
        }
    }
}