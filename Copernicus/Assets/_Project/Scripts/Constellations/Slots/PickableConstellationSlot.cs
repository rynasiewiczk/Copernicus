namespace _Project.Scripts.Constellations
{
    using LazySloth.Utilities;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class PickableConstellationSlot : MonoBehaviour
    {
        [SerializeField] private Physics2dButton _button;
        [SerializeField] private Transform _container;

        private Constellation _constellation;

        public bool HasNotDroppedConstellation => _constellation != null && !_constellation.IsDroppedOnBoard;
        
        private void OnEnable()
        {
            _button.OnClicked += TryPickUp;
            PlayerController.Instance.OnUnpickedDraggable += ResetConstellationAsChild;
        }
        
        private void OnDisable()
        {
            _button.OnClicked -= TryPickUp;

            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.OnUnpickedDraggable -= ResetConstellationAsChild;
            }
        }
        
        private void TryPickUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (!HasNotDroppedConstellation)
            {
                return;
            }

            PlayerController.Instance.TryPickUpDraggable(_constellation);
        }
        
        public void SetConstellation(Constellation constellation)
        {
            _constellation = constellation;
            _constellation.SetActive(true);
            ResetConstellationAsChild(_constellation);
        }
        
        private void ResetConstellationAsChild(IDraggable draggable)
        {
            if (_constellation == null)
            {
                Debug.LogError("Group is null");
                return;
            }

            if (draggable is not Constellation c || _constellation != c)
            {
                //not my constellation, ignoring
                return;
            }

            _constellation.SetParent(_container);
        }
    }
}