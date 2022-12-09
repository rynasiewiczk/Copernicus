namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Group : MonoBehaviour
    {
        [SerializeField] private List<Block> _blocks;

        public IReadOnlyList<Block> Blocks => _blocks;

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
    }
}