namespace _Project.Scripts.Effects
{
    using System;
    using DG.Tweening;
    using LazySloth.Utilities;
    using TMPro;
    using UnityEngine;

    public class FloatingTextEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private AnimationCurve _upCurve;
        [SerializeField] private AnimationCurve _fadeCurve;
        [SerializeField] private float _movement = 3;
        [SerializeField] private float _duration = 1;

        public void Setup(Vector2 position, string text, Action onComplete)
        {
            transform.position = position;
            _text.color = _text.color.SetAlpha(1);
            _text.text = text;

            gameObject.SetActive(true);
            
            transform.DOMoveY(position.y + _movement, _duration).SetEase(_upCurve);
            _text.DOFade(0, _duration + .1f).SetEase(_fadeCurve).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
    }
}