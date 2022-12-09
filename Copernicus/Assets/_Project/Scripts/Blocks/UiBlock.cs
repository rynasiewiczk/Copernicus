namespace _Project.Scripts
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class UiBlock : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridPosition;
        public RectTransform RectTransform => transform as RectTransform;

        private Vector2 Size => RectTransform.sizeDelta;

        public Vector2Int GetGridPosition()
        {
            return _gridPosition;
        }

        [Button]
        public void Validate()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            RectTransform.anchoredPosition = GetGridPosition() * Size;
            gameObject.name = $"UiBlock_{GetGridPosition().x}_{GetGridPosition().y}";
        }
    }
}