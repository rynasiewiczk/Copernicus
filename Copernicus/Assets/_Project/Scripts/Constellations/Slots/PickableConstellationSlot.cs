namespace _Project.Scripts.Constellations
{
    using DG.Tweening;
    using UnityEngine;

    public class PickableConstellationSlot : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private float _zoomedInScale = .5f;
        [SerializeField] private float _zoomSpeed = .2f;

        private Constellation _constellation;

        public bool HasNotDroppedConstellation => _constellation != null && !_constellation.IsDroppedOnBoard;
        
        private void OnEnable()
        {
            PlayerController.Instance.OnUnpickedDraggable += ResetConstellationAsChild;
        }
        
        private void OnDisable()
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.OnUnpickedDraggable -= ResetConstellationAsChild;
            }
        }

        private void OnMouseDown()
        {
            if (!HasNotDroppedConstellation)
            {
                return;
            }

            PlayerController.Instance.TryPickUpDraggable(_constellation);
        }

        private void OnMouseEnter()
        {
            if(PlayerController.Instance.HasDraggable) { return; }
            if(GameController.Instance.HasInteractionIgnoreReason) { return;}
            
            _container.DOScale(_zoomedInScale, _zoomSpeed);
        }

        private void OnMouseExit()
        {
            _container.DOScale(0.3f, _zoomSpeed);
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