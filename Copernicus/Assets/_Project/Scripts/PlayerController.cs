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

            CheckIfCanPutOnBoard();

            if (Input.GetMouseButtonDown(0))
            {
                TryPutOnBoard();
            }

            else if (Input.GetMouseButtonDown(1))
            {
                Unpick();
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                TryRotateLeft();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                TryRotateRight();
            }
        }

        private void TryPutOnBoard()
        {
            var canPutOnBoard = CheckIfCanPutOnBoard();
            if (canPutOnBoard)
            {
                BoardController.Instance.PutGroupOnBoard(_currentGroup);
                _currentGroup = null;
            }
        }

        private bool CheckIfCanPutOnBoard()
        {
            if (_currentGroup == null)
            {
                return false;
            }

            var canPutOnBoard = BoardController.Instance.IsPositionValidForGroup(_currentGroup);
            _currentGroup.SetOverBoardValidPositionView(canPutOnBoard);
            return canPutOnBoard;
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
                return;
            }

            _currentGroup.ResetRotation();
            OnUnpickedGroup?.Invoke(_currentGroup);
            _currentGroup = null;
        }

        private void TryRotateLeft()
        {
            if (_currentGroup == null)
            {
                return;
            }

            _currentGroup.RotateLeft();
        }

        private void TryRotateRight()
        {
            if (_currentGroup == null)
            {
                return;
            }

            _currentGroup.RotateRight();
        }
    }
}