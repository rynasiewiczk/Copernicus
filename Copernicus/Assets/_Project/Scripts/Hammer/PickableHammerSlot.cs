namespace _Project.Scripts.Hammer
{
    using System;
    using DG.Tweening;
    using UI;
    using UnityEngine;

    public class PickableHammerSlot : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private HammerObject hammerObject;

        private void OnEnable()
        {
            PlayerController.Instance.OnUnpickedDraggable += ResetHammerAsChild;
        }
        
        private void OnDisable()
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.OnUnpickedDraggable -= ResetHammerAsChild;
            }
        }

        private void OnMouseDown()
        {
            if (hammerObject.IsDragged.Value)
            {
                return;
            }

            PlayerController.Instance.TryPickUpDraggable(hammerObject);
        }

        private void OnMouseEnter()
        {
            if(PlayerController.Instance.HasDraggable) { return; }
            if(GameController.Instance.HasInteractionIgnoreReason) { return;}
            
            _container.DOScale(0.6f, 0.3f);
        }

        private void OnMouseExit()
        {
            _container.DOScale(0.5f, 0.3f);
        }

        private void ResetHammerAsChild(IDraggable draggable)
        {
            if (draggable is not HammerObject hammer)
            {
                return;
            }

            hammer.SetParent(_container);
            hammer.transform.localScale = Vector3.one;
        }
    }
}