namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constellations;
    using DG.Tweening;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class GameController : SingletonBehaviour<GameController>
    {
        public event Action<Group> OnGroupShowing;
        public event Action<Constellation> OnConstellationShowing;

        [SerializeField] private Transform _deskRoot;

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
        private readonly List<Constellation> _droppedConstellations = new();

        public int GroupsQueueCount => _groupsQueue.Count;
        public IReadOnlyList<Constellation> DroppedConstellations => _droppedConstellations;

        public List<InteractionIgnoreReason> InteractionIgnoreReasons = new();
        public bool HasInteractionIgnoreReason => InteractionIgnoreReasons.Any();

        private void Start() //start game
        {
            FillUpConstellations();
            
            CreateGroups(_initialGroupsSize);
            FillUpGroupsToShow();
            
            RefreshConstellationsPossibility();
        }

        public void PutGroupOnMap(Group group)
        {
            _groupsToShow.Remove(group);
            FillUpGroupsToShow();
            RefreshConstellationsPossibility();
            CheckForGameOver();
        }

        public void PutConstellationOnMap(Constellation constellation)
        {
            _currentConstellations.Remove(constellation);
            _droppedConstellations.Add(constellation);
            CreateGroups(2); //todo: define based on constellations
            FillUpGroupsToShow();
            FillUpConstellations();
            RefreshConstellationsPossibility();
            CheckForGameOver();

            constellation.HandlePutOnMap();
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
                
                OnConstellationShowing?.Invoke(newConstellation);
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

        private void RefreshConstellationsPossibility()
        {
            foreach (var constellation in _currentConstellations)
            {
                constellation.MarkPossibleToPutOnBoard(BoardController.Instance.IsConstellationPossibleToPutOnBoard(constellation));
            }
        }

        private void CheckForGameOver()
        {
            if(_groupsToShow.Any()) { return; }
            if(_currentConstellations.Any(x => x.IsPossibleToPutOnBoard)) { return; }

            var allPositions = BoardController.Instance.BlocksOnBoard.Select(x => x.GetGridPosition()).ToList();
            var byX = allPositions.OrderBy(v => v.x).ToList();
            var minX = byX.First().x;
            var maxX = byX.Last().x;
            var byY = allPositions.OrderBy(v => v.y).ToList();
            var minY = byY.First().y;
            var maxY = byY.Last().y;

            var center = new Vector2((minX + maxX) / 2f, (minY + maxY) / 2f);
            var xExtend = Mathf.Abs(maxX - minY);
            var yExtend = Mathf.Abs(maxY - minY);
            CameraController.Instance.ShowEndSequence(center, xExtend, yExtend);

            _deskRoot.DOScale(100f, 20f);
        }
    }
}