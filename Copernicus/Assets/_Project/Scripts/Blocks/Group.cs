namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
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
        }
    }
}