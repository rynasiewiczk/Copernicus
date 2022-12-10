namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constellations;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class GameController : SingletonBehaviour<GameController>
    {
        public event Action<Group> OnGroupShowing; 

        [Header("Groups")]
        [SerializeField] private GroupsCatalog _groupsCatalog;
        [SerializeField] private int _initialGroupsSize = 10;
        [SerializeField] private int _maxGropusToShow = 3;

        [Header("Constellations")]
        [SerializeField] private ConstellationsCatalog _constellationsCatalog;
        [SerializeField] private int _maxConstellations = 3;

        private readonly Queue<Group> _groupsQueue = new();
        private readonly List<Group> _groupsToShow = new();

        private readonly List<Constellation> _currentConstellations = new();

        public int GroupsQueueCount => _groupsQueue.Count;

        private void Start() //start game
        {
            FillUpConstellations();
            
            CreateGroups(_initialGroupsSize);
            FillUpGroupsToShow();
        }

        public void PutGroupOnMap(Group group)
        {
            _groupsToShow.Remove(group);
            FillUpGroupsToShow();
        }

        public void PutConstellationOnMap(Constellation constellation)
        {
            _currentConstellations.Remove(constellation);
            CreateGroups(2); //todo: define based on constellations
            FillUpGroupsToShow();
            FillUpConstellations();
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
                    
                    OnGroupShowing?.Invoke(group);
                    ////refresh ui?
                }
            }
        }

        private void FillUpConstellations()
        {
            var missingConstellations = _maxConstellations - _currentConstellations.Count;
            for (var i = 0; i < missingConstellations; i++)
            {
                var constellation = _constellationsCatalog.GetRandomConstellation();
                var newConstellation = Instantiate(constellation);
                _currentConstellations.Add(newConstellation);
            }
        }
        
        private void CreateGroups(int groupsCount)
        {
            for (var i = 0; i < groupsCount; i++)
            {
                var groupToCreate = _groupsCatalog.GetRandomGroup();
                var newGroup = Instantiate(groupToCreate);
                newGroup.SetActive(false);
                var maxStars = newGroup.Blocks.Count;
                var numberOfStars = Random.Range(1, maxStars);
                newGroup.Init(numberOfStars);
                _groupsQueue.Enqueue(newGroup);
            }
        }
    }
}