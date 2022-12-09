namespace _Project.Scripts.UI
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class PickableGroupSlot : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _container;
        
        public UiGroup UiGroup { get; private set; }

        public bool HasGroup => UiGroup != null;

        private void OnEnable()
        {
            _button.onClick.AddListener(TryPickUpGroup);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(TryPickUpGroup);
        }

        private void TryPickUpGroup()
        {
            Debug.Log($"<color=green>try pick up {UiGroup.name}</color>");
        }

        public void SetGroup(UiGroup group)
        {
            UiGroup = group;
            UiGroup.transform.SetParent(_container, false);
        }

        public bool TryGetGroup(out Group group)
        {
            if (HasGroup)
            {
                group = UiGroup.Group;
                return true;
            }

            group = null;
            return false;
        }

        public void Clear()
        {
            Destroy(UiGroup.gameObject);
            UiGroup = null;
        }
    }
}