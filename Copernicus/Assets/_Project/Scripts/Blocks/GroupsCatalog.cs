namespace _Project.Scripts
{
    using System.Collections.Generic;
    using LazySloth.Utilities;
    using UnityEngine;

    public class GroupsCatalog : ScriptableObject
    {
        [SerializeField] private Group _singleElementGroup;
        [SerializeField] private List<Group> _groups;

        public Group SingleElementGroup => _singleElementGroup;
        public IReadOnlyList<Group> Groups => _groups;

        public Group GetRandomGroup() => _groups.GetRandom();
    }
}