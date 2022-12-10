namespace _Project.Scripts
{
    using System;
    using Constellations;
    using DG.Tweening;
    using LazySloth.Utilities;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Utils;

    public class PlayerController : SingletonBehaviour<PlayerController>
    {
        public event Action<IDraggable> OnUnpickedDraggable;

        [SerializeField] private Camera _gameplayCamera;

        private IDraggable _currentDraggable;

        public IDraggable CurrentDraggable => _currentDraggable;
        public bool HasDraggable => CurrentDraggable != null;

        private bool _didJustChangeDraggable;

        public bool TryPickUpDraggable(IDraggable draggable)
        {
            if (HasDraggable || _didJustChangeDraggable)
            {
                return false;
            }

            SetChangeDraggableFlag();

            _currentDraggable = draggable;
            _currentDraggable.Root.ChangeLayerToAboveDesk();
            _currentDraggable.SetParentAndScale(null, Vector3.one);
            
            return true;
        }
        
        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     GameController.Instance.gameObject.SetActive(false);
            // }
            
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

            else if (_currentDraggable is Group group)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    TryRotateRight(group);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    TryRotateLeft(group);
                }
            }
        }

        private void TryPutOnBoard()
        {
            if (_didJustChangeDraggable)
            {
                return;
            }
            
            var canPutOnBoard = CheckIfCanPutOnBoard();
            if (canPutOnBoard)
            {
                SetChangeDraggableFlag();

                if (_currentDraggable is Group group)
                {
                    BoardController.Instance.PutGroupOnBoard(group);
                }

                if (_currentDraggable is Constellation constellation)
                {
                    BoardController.Instance.PutConstellationOnBoard(constellation);
                }

                _currentDraggable.Root.ChangeLayerToDefault();
                _currentDraggable = null;
            }
        }

        private bool CheckIfCanPutOnBoard()
        {
            if (_currentDraggable == null)
            {
                return false;
            }

            if (_currentDraggable is Group group)
            {
                var canPutOnBoard = BoardController.Instance.IsPositionValidForGroup(group);
                group.SetOverBoardValidPositionView(canPutOnBoard);
                return canPutOnBoard;
            }

            if (_currentDraggable is Constellation constellation)
            {
                var canPutOnBoard = BoardController.Instance.IsPositionValidForConstellation(constellation);
                return canPutOnBoard;
            }

            return false;
        }

        private void MoveGroupWithCursor()
        {
            if (_currentDraggable == null)
            {
                return;
            }

            var mousePosition = Input.mousePosition;
            var worldPosition = _gameplayCamera.ScreenToWorldPoint(mousePosition).SetZ(0);
            _currentDraggable.SetWorldPosition(worldPosition);
        }

        [Button]
        public void Unpick()
        {
            if (_currentDraggable == null || _didJustChangeDraggable)
            {
                return;
            }

            SetChangeDraggableFlag();

            _currentDraggable.Root.ChangeLayerToDesk();
            _currentDraggable.ResetRotation();
            OnUnpickedDraggable?.Invoke(_currentDraggable);
            _currentDraggable = null;
        }

        private void SetChangeDraggableFlag()
        {
            _didJustChangeDraggable = true;
            DOVirtual.DelayedCall(.1f, () => _didJustChangeDraggable = false);
        }
        
        private void TryRotateLeft(Group group)
        {
            group.RotateLeft();
        }

        private void TryRotateRight(Group group)
        {
            group.RotateRight();
        }
    }
}