namespace _Project.Scripts.UI
{
    using System;
    using UnityEngine;

    [Serializable]
    public class WindowAnimationData
    {
        [SerializeField] private float _showDuration = .3f;
        [SerializeField] private float _hideDuration = .3f;

        [SerializeField] private AnimationCurve _showCurve;
        [SerializeField] private AnimationCurve _hideCurve;

        public float ShowDuration => _showDuration;
        public float HideDuration => _hideDuration;
        public AnimationCurve ShowCurve => _showCurve;
        public AnimationCurve HideCurve => _hideCurve;
    }
}