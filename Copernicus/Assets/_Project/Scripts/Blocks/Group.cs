namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Group : MonoBehaviour
    {
        [SerializeField] private List<Block> _blocks;

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
                block.DropOnMap();
            }

            //despawn this?
        }

        public void RotateLeft() => transform.eulerAngles += Vector3.left * 90;

        public void RotateRight() => transform.eulerAngles += Vector3.right * 90;

        public void ResetRotation() => transform.eulerAngles = Vector3.zero;

        ///////////

        [Button]
        private void Validate()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            foreach (var block in _blocks)
            {
                block.Validate();
            }
        }

        public void SetActive(bool active) => gameObject.SetActive(active);

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
            transform.localPosition = Vector3.zero;
        }

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}