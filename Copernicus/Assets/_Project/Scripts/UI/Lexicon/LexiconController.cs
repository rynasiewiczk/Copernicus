namespace _Project.Scripts.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class LexiconController : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            UiController.Instance.SetWindowOpen(true);
            _closeButton.onClick.AddListener(Close);
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
            gameObject.SetActive(false);
        }
    }
}