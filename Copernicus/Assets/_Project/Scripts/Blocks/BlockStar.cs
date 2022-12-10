namespace _Project.Scripts
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class BlockStar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Sprite _unusedStarSprite;
        [SerializeField] private Sprite _usedStarSprite;

        [SerializeField] private float _hideDuration = .075f;
        [SerializeField] private float _hideRotation = 360;
        [SerializeField] private float _showDuration = .3f;
        [SerializeField] private float _showRotation = 360;
        [SerializeField] private AnimationCurve _showScaleCurve;

        [SerializeField] private ParticleSystem _idleParticleSystem;
        [SerializeField] private ParticleSystem _transitionParticleSystem;
        
        public bool IsDroppedOnBoard { get; private set; }
        public bool IsAlreadyUsed { get; private set; }

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.M))
        //     {
        //         MarkUsed();
        //     }
        //
        //     if (Input.GetKeyDown(KeyCode.N))
        //     {
        //         IsAlreadyUsed = false;
        //         _spriteRenderer.sprite = _unusedStarSprite;
        //     }
        // }

        private void Start()
        {
            _idleParticleSystem.gameObject.SetActive(false);
        }

        public void MarkDroppedOnBoard()
        {
            IsDroppedOnBoard = true;
            _idleParticleSystem.gameObject.SetActive(true);
        }
        
        public void MarkUsed()
        {
            IsAlreadyUsed = true;

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(0, _hideDuration));
            sequence.Join(transform.DORotate(Vector3.forward * _hideRotation, _hideDuration, RotateMode.LocalAxisAdd).OnComplete(() =>
            {
                _spriteRenderer.sprite = _usedStarSprite;
                _transitionParticleSystem.Play();
            }));
            sequence.Append(transform.DOScale(1, _showDuration).SetEase(_showScaleCurve));
            sequence.Join(transform.DORotate(Vector3.forward * _showRotation, _showDuration, RotateMode.LocalAxisAdd));
        }
    }
}