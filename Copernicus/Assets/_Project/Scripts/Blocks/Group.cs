namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Group : MonoBehaviour, IDraggable
    {
        [SerializeField] private List<Block> _blocks;

        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _invalidPlaceColor;
        [SerializeField] private Color _validPlaceColor;

        private float _rotationDuration = .15f;
        private float _dropOnMapDuration = .15f;

        public Transform Root => gameObject.transform;
        public IReadOnlyList<Block> Blocks => _blocks;
        public bool IsOnMap => _blocks.Any(x => x.IsOnMap);

        public void Init(int numberOfStars)
        {
            var blocksInRandom = _blocks.OrderBy(x => Guid.NewGuid()).ToList();
            for (var i = 0; i < blocksInRandom.Count; i++)
            {
                blocksInRandom[i].Init(i < numberOfStars);
            }
        }

        public void DropOnMap()
        {
            var reason = new InteractionIgnoreReason("Placing block");
            GameController.Instance.InteractionIgnoreReasons.Add(reason);
            DOVirtual.DelayedCall(_dropOnMapDuration, () => GameController.Instance.InteractionIgnoreReasons.Remove(reason));

            foreach (var block in _blocks)
            {
                block.SetColor(_defaultColor);
                block.DropOnMap(_dropOnMapDuration);
            }
        }

        public void ChangeLayer(int newLayer)
        {
            var root = gameObject.transform;
            var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = newLayer;
            }
        }

        public void RotateLeft()
        {
            var interactionIgnoreReason = new InteractionIgnoreReason("Rotating group");
            GameController.Instance.InteractionIgnoreReasons.Add(interactionIgnoreReason);
            transform.DORotate(new Vector3(0, 0, -90), _rotationDuration, RotateMode.WorldAxisAdd).OnComplete(() =>
            {
                GameController.Instance.InteractionIgnoreReasons.Remove(interactionIgnoreReason);
            });
        }

        public void RotateRight()
        {
            var interactionIgnoreReason = new InteractionIgnoreReason("Rotating group");
            GameController.Instance.InteractionIgnoreReasons.Add(interactionIgnoreReason);
            transform.DORotate(new Vector3(0, 0, 90), _rotationDuration, RotateMode.WorldAxisAdd).OnComplete(() =>
            {
                GameController.Instance.InteractionIgnoreReasons.Remove(interactionIgnoreReason);
            });
        }

        public void ResetRotation() => transform.eulerAngles = Vector3.zero;

        public void SetParentAndScale(Transform parent, Vector3 scale)
        {
            transform.SetParent(parent);
            transform.localScale = scale;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void ResetColor() => _blocks.ForEach(x => x.SetColor(_defaultColor));

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetOverBoardValidPositionView(bool canPutOnBoard)
        {
            foreach (var block in _blocks)
            {
                block.SetColor(canPutOnBoard ? _validPlaceColor : _invalidPlaceColor);
            }
        }

        public void SetActive(bool active) => gameObject.SetActive(active);

        ///////////

        [Button]
        private void Validate()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            _blocks.Clear();
            _blocks = GetComponentsInChildren<Block>().ToList();

            foreach (var block in _blocks)
            {
                block.Validate();
            }
        }
    }
}