namespace _Project.Scripts.UI
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;
    using UnityEngine.UI;

    public class PickableGroupSlot : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _container;
        [SerializeField] private UiGroup _uiGroup;

        [SerializeField] private CanvasGroup _groupCanvasGroup;

        public UiGroup UiGroup => _uiGroup;

        public bool HasGroup => UiGroup.Group != null;

        private void OnEnable()
        {
            _button.onClick.AddListener(TryPickUpGroup);
            PlayerController.instance.OnUnpickedGroup += ShowGroup;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(TryPickUpGroup);
            PlayerController.instance.OnUnpickedGroup -= ShowGroup;
        }

        public void SetGroup(Group group)
        {
            _uiGroup.Initialize(group);
        }

        public void ResetGroup()
        {
            _uiGroup.Reset();
        }

        private void TryPickUpGroup()
        {
            var didPickUp = PlayerController.instance.TryPickUpGroup(UiGroup.Group);
            if (didPickUp)
            {
                HideGroup();
            }
        }

        private void HideGroup()
        {
            _groupCanvasGroup.alpha = 0;
        }

        private void ShowGroup(Group _)
        {
            _groupCanvasGroup.alpha = 1;
        }

        public void Clear()
        {
            ResetGroup();
        }
    }
}