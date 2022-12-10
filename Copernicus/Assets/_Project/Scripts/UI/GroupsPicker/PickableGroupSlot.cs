namespace _Project.Scripts.UI
{
    using System.Linq;
    using LazySloth.Utilities;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class PickableGroupSlot : MonoBehaviour
    {
        [SerializeField] private Physics2dButton _button;
        [SerializeField] private Transform _container;

        [SerializeField] private float _offset = -.5f;

        public Group Group { get; private set; }

        public bool HasGroup => Group != null;

        private void OnEnable()
        {
            _button.OnClicked += TryPickUpGroup;
            PlayerController.Instance.OnUnpickedGroup += ResetGroupAsChild;
        }

        private void OnDisable()
        {
            _button.OnClicked -= TryPickUpGroup;
            PlayerController.Instance.OnUnpickedGroup -= ResetGroupAsChild;
        }

        public void SetGroup(Group group)
        {
            Group = group;
            ResetGroupAsChild(Group);
        }

        private void ResetGroupAsChild(Group _)
        {
            if (Group == null)
            {
                Debug.LogError("Group is null");
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

        public void Clear()
        {
            Group = null;
        }

        private void TryPickUpGroup()
        {
            var didPickUp = PlayerController.Instance.TryPickUpGroup(Group);
            if (didPickUp)
            {
                //HideGroup();
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