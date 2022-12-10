namespace LazySloth.Utilities
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// In order for this to work scene camera has to have Physics2dRaycaster component attached to it
    /// and target for layer same as object with this component
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Physics2dButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action<PointerEventData> OnClicked;
        protected virtual void FireClicked(PointerEventData eventData) => OnClicked?.Invoke(eventData);

        private bool _interactable = true;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if(!_interactable) { return; }
            FireClicked(eventData);
        }

        public void SetInteractable(bool interactable) => _interactable = interactable;

        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}