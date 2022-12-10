namespace _Project.Scripts
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class PlayerController : SingletonBehaviour<PlayerController>
    {
        public event Action<Group> OnUnpickedGroup; 

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