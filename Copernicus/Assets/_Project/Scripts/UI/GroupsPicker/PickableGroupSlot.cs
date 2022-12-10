namespace _Project.Scripts.UI
{
    using System;
    using System.Linq;
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Random = System.Random;

    public class PickableGroupSlot : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        [SerializeField] private float _offset = -.5f;

        public Group Group { get; private set; }

        public bool HasNotDroppedGroup => Group != null && !Group.IsOnMap;

        private void OnEnable()
        {
            PlayerController.Instance.OnUnpickedDraggable += ResetGroupAsChild;
        }

        private void OnDisable()
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.OnUnpickedDraggable -= ResetGroupAsChild;
            }
        }

        private void OnMouseDown()
        {
            if (!HasNotDroppedGroup)
            {
                return;
            }

            PlayerController.Instance.TryPickUpDraggable(Group);
        }

        private void OnMouseEnter()
        {
            if(PlayerController.Instance.HasDraggable) { return; }
            if(GameController.Instance.HasInteractionIgnoreReason) { return;}

            _container.DOScale(0.7f, 0.3f);
        }

        private void OnMouseExit()
        {
            _container.DOScale(0.5f, 0.3f);
        }

        public void SetGroup(Group group)
        {
            Group = group;
            Group.SetActive(true);
            ResetGroupAsChild(Group, true);
        }

        private void ResetGroupAsChild(IDraggable draggable) => ResetGroupAsChild(draggable, false);

        private void ResetGroupAsChild(IDraggable draggable, bool bump)
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
            CenterGroup(bump);
        }

        private void CenterGroup(bool bump)
        {
            if (Group == null)
            {
                return;
            }

            var groupSizeX = Group.Blocks.Select(x => x.GetLocalGridPosition().x).Distinct().Count();
            var groupSizeY = Group.Blocks.Select(x => x.GetLocalGridPosition().y).Distinct().Count();

            var xOffset = groupSizeX % 2 == 0 ? _offset : 0;
            var yOffset = groupSizeY % 2 == 0 ? _offset : 0;

            Group.SetLocalPosition(new Vector3(xOffset, yOffset, 0));
            
            if (bump)
            {
                Group.PlayBump();
            }
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