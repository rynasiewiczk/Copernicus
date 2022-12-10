namespace _Project.Scripts
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class GameController : SingletonBehaviour<GameController>
    {
        [SerializeField] private GroupsCatalog _groupsCatalog;
        [SerializeField] private int _initialGroupsSize = 10;
        [SerializeField] private int _maxGropusToShow = 3;

        private readonly Queue<Group> _groupsQueue = new();
        private List<Group> _groupsToShow = new();

        public int GroupsQueueCount => _groupsQueue.Count;
        
        private void Start() //start game
        {
            CreateGroups(_initialGroupsSize);
            FillUpGroupsToShow();
        }

        public void PutGroupOnMap(Group group)
        {
            _groupsToShow.Remove(group);
            FillUpGroupsToShow();
        }

        private void FillUpGroupsToShow()
        {
            var missingGroups = _maxGropusToShow - _groupsToShow.Count;
            for (var i = 0; i < missingGroups; i++)
            {
                if (_groupsQueue.Any())
                {
                    var group = _groupsQueue.Dequeue();
                    _groupsToShow.Add(group);
                    //refresh ui
                }
            }
        }
        
        private void CreateGroups(int groupsCount)
        {
            for (var i = 0; i < groupsCount; i++)
            {
                var groupToCreate = _groupsCatalog.GetRandomGroup();
                var newGroup = Instantiate(groupToCreate);
                newGroup.gameObject.SetActive(false);
                var maxStars = newGroup.Blocks.Count;
                var numberOfStars = Random.Range(1, maxStars);
                newGroup.Init(numberOfStars);
                _groupsQueue.Enqueue(newGroup);
            }
        }
    }
}