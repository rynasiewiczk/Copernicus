namespace _Project.Scripts.UI.HowTo
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.UI;

    public class HowToController : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private WindowAnimationData _animationData;

        private bool _isAnimating;
        
        private void OnEnable()
        {
            UiController.Instance.SetWindowOpen(true);
            _closeButton.onClick.AddListener(Close);

            _isAnimating = true;
            DOVirtual.Float(0, 1, _animationData.ShowDuration, value => transform.localScale = Vector3.one * value).SetEase(_animationData.ShowCurve).OnComplete(() => _isAnimating = false);
        }

        private void OnDisable()
        {
            UiController.Instance.SetWindowOpen(false);
            _closeButton.onClick.RemoveListener(Close);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
        
        private void Close()
        {
            _isAnimating = true;

            DOVirtual.Float(0, 1, _animationData.HideDuration, value => transform.localScale = Vector3.one * value).SetEase(_animationData.HideCurve).OnComplete(() =>
            {
                _isAnimating = false;
                gameObject.SetActive(false);
            });
        }
    }
}