namespace _Project.Scripts
{
    using System;
    using DG.Tweening;
    using LazySloth.Utilities;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class BlockStar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Sprite _unusedStarSprite;
        [SerializeField] private Sprite _usedStarSprite;

        [SerializeField] private float _hideDuration = .075f;
        [SerializeField] private float _hideRotation = 360;
        [SerializeField] private float _showDuration = .3f;
        [SerializeField] private FloatRange _showRotationRange = new(700, 740);
        
        [SerializeField] private AnimationCurve _showScaleCurve;

        [SerializeField] private ParticleSystem _idleParticleSystem;
        [SerializeField] private ParticleSystem _transitionParticleSystem;

        [SerializeField] private AnimationCurve _idleScaleCurve;

        public bool IsDroppedOnBoard { get; private set; }
        public bool IsAlreadyUsed { get; private set; }

        private bool _isTransitioning;
        private float _idleAnimationTimer;

        private void Start()
        {
            _idleParticleSystem.gameObject.SetActive(false);
            _idleAnimationTimer = Random.Range(0, 1);
        }

        private void Update()
        {
            if (!_isTransitioning)
            {
                _idleAnimationTimer += Time.deltaTime;
                var lerp = (Mathf.Sin(_idleAnimationTimer) + 1) / 2;
                var scale = _idleScaleCurve.Evaluate(lerp);
                transform.localScale = Vector3.one * scale;
            }

            // if (Input.GetKeyDown(KeyCode.M))
            // {
            //     MarkUsed();
            // }
            //
            // if (Input.GetKeyDown(KeyCode.N))
            // {
            //     IsAlreadyUsed = false;
            //     _spriteRenderer.sprite = _unusedStarSprite;
            // }
        }

        public void MarkDroppedOnBoard()
        {
            IsDroppedOnBoard = true;
            _idleParticleSystem.gameObject.SetActive(true);
        }

        public void MarkUsed()
        {
            _isTransitioning = true;
            IsAlreadyUsed = true;

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(0, _hideDuration));
            sequence.Join(transform.DORotate(Vector3.forward * _hideRotation, _hideDuration, RotateMode.LocalAxisAdd).OnComplete(() =>
            {
                _spriteRenderer.sprite = _usedStarSprite;
                _transitionParticleSystem.Play();
            }));
            sequence.Append(transform.DOScale(1, _showDuration).SetEase(_showScaleCurve));
            sequence.Join(transform.DORotate(Vector3.forward * _showRotationRange.GetRandom(), _showDuration, RotateMode.LocalAxisAdd));
            sequence.OnComplete(() => _isTransitioning = false);
        }
    }
}