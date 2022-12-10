namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LazySloth.Utilities;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Group : MonoBehaviour, IDraggable
    {
        [SerializeField] private List<Block> _blocks;

        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _invalidPlaceColor;
        [SerializeField] private Color _validPlaceColor;

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
            foreach (var block in _blocks)
            {
                block.SetColor(_defaultColor);
                block.DropOnMap();
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
        
        public void RotateLeft() => transform.eulerAngles += new Vector3(0, 0, -90);

        public void RotateRight() => transform.eulerAngles += new Vector3(0, 0, 90);

        public void ResetRotation() => transform.eulerAngles = Vector3.zero;

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
            transform.localPosition = Vector3.zero;
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