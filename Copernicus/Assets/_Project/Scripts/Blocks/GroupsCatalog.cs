namespace _Project.Scripts
{
    using System.Collections.Generic;
    using LazySloth.Utilities;
    using UnityEngine;

    public class GroupsCatalog : ScriptableObject
    {
        [SerializeField] private List<Group> _groups;
        
        public IReadOnlyList<Group> Groups => _groups;

        public Group GetRandomGroup() => _groups.GetRandom();
    }
}