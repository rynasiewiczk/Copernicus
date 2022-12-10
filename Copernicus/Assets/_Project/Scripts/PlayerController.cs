namespace _Project.Scripts
{
    using System;
    using LazySloth.Utilities;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class PlayerController : SingletonBehaviour<PlayerController>
    {
        public event Action<Group> OnUnpickedGroup;

        [SerializeField] private Camera _gameplayCamera;
        
        private Group _currentGroup;

        public Group CurrentGroup => _currentGroup;
        public bool HasGroup => CurrentGroup != null;

        public bool TryPickUpGroup(Group group)
        {
            if (HasGroup)
            {
                return false;
            }

            _currentGroup = group;
            return true;
        }

        private void Update()
        {
            MoveGroupWithCursor();

            if (Input.GetMouseButtonDown(1))
            {
                Unpick();
            }
        }

        private void MoveGroupWithCursor()
        {
            if (_currentGroup == null)
            {
                return;
            }

            var mousePosition = Input.mousePosition;
            var worldPosition = _gameplayCamera.ScreenToWorldPoint(mousePosition).SetZ(0);
            _currentGroup.SetWorldPosition(worldPosition);
        }

        [Button]
        public void Unpick()
        {
            if (_currentGroup == null)
            {
                Debug.LogError("There is no group to unpick");
                return;
            }
            
            OnUnpickedGroup?.Invoke(_currentGroup);
            _currentGroup = null;
        }
    }
}