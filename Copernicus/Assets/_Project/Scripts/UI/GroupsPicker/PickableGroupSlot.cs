namespace _Project.Scripts.UI
{
    using System.Linq;
    using LazySloth.Utilities;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class PickableGroupSlot : MonoBehaviour
    {
        [SerializeField] private Physics2dButton _button;
        [SerializeField] private Transform _container;

        [SerializeField] private float _offset = -.5f;

        public Group Group { get; private set; }

        public bool HasNotDroppedGroup => Group != null && !Group.IsOnMap;

        private void OnEnable()
        {
            _button.OnClicked += TryPickUp;
            PlayerController.Instance.OnUnpickedDraggable += ResetGroupAsChild;
        }

        private void OnDisable()
        {
            _button.OnClicked -= TryPickUp;

            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.OnUnpickedDraggable -= ResetGroupAsChild;
            }
        }

        private void TryPickUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (!HasNotDroppedGroup)
            {
                return;
            }

            PlayerController.Instance.TryPickUpDraggable(Group);
        }
        
        public void SetGroup(Group group)
        {
            Group = group;
            Group.SetActive(true);
            ResetGroupAsChild(Group);
        }

        private void ResetGroupAsChild(IDraggable draggable)
        {
            if (Group == null)
            {
                Debug.LogError("Group is null");
                return;
            }

            if (draggable is not Group group || Group != group)
            {
                //not my group, ignoring
                return;
            }

            Group.SetParent(_container);
            CenterGroup();
            Group.ResetColor();
        }

        private void CenterGroup()
        {
            if (Group == null)
            {
                return;
            }

            var groupSizeX = Group.Blocks.Select(x => x.GetGridPosition().x).Distinct().Count();
            var groupSizeY = Group.Blocks.Select(x => x.GetGridPosition().y).Distinct().Count();

            var xOffset = groupSizeX % 2 == 0 ? _offset : 0;
            var yOffset = groupSizeY % 2 == 0 ? _offset : 0;
            Group.SetWorldPosition(Group.transform.position + new Vector3(xOffset, yOffset, 0));
        }

        //debug
        [Button]
        public void DestroyGroup()
        {
            if (Group != null)
            {
                Destroy(Group.gameObject);
            }
        }
    }
}