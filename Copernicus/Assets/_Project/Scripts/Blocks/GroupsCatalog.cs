namespace _Project.Scripts
{
    using System.Collections.Generic;
    using LazySloth.Utilities;
    using UnityEngine;

    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class GroupsCatalog : ScriptableObject
    {
        [SerializeField] private List<Group> _groups;
        [SerializeField] private List<UiGroup> _uiGroups;
        
        public IReadOnlyList<Group> Groups => _groups;
        public IReadOnlyList<UiGroup> UiGroups => _uiGroups;

        public Group GetRandomGroup() => _groups.GetRandom();
        public UiGroup GetRandomUiGroup() => UiGroups.GetRandom();
    }
}