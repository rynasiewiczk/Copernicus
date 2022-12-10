namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using Constellations;
    using UnityEngine;

    public class BoardController : SingletonBehaviour<BoardController>
    {
        [SerializeField] private GroupsCatalog _groupsCatalog;
        
        private readonly List<Vector2Int> _neighborsPositions = new()
        {
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.right
        };

        private List<Block> _blocksOnBoard = new();

        private void Start()
        {
            SpawnInitialBlock();
        }

        private void SpawnInitialBlock()
        {
            var singleElementGroupPrefab = _groupsCatalog.SingleElementGroup;
            var instance = Instantiate(singleElementGroupPrefab);
            instance.SetWorldPosition(Vector3.zero);
            PutGroupOnBoard(instance);
        }

        public bool IsPositionValidForGroup(Group group)
        {
            //check if all places empty
            foreach (var blockInGroup in group.Blocks)
            {
                if (TryGetBlockAtPosition(blockInGroup.GetGridPosition(), out var blockOnMap))
                {
                    return false; //there is block at this position already
                }
            }
            
            //check if there is neighbor 
            foreach (var blockInGroup in group.Blocks)
            {
                var blockPosition = blockInGroup.GetGridPosition();
                foreach (var neighborsPosition in _neighborsPositions)
                {
                    var positionToCheck = blockPosition + neighborsPosition;
                    if (TryGetBlockAtPosition(positionToCheck, out var blockOnMap))
                    {
                        return true; //only one neighbor is required
                    }
                }
            }
            
            return false;
        }

        public void PutGroupOnBoard(Group group)
        {
            group.DropOnMap();
            
            foreach (var blockInGroup in group.Blocks)
            {
                _blocksOnBoard.Add(blockInGroup);
            }
            
            GameController.Instance.PutGroupOnMap(group);
        }

        public bool IsPositionValidForConstellation(Constellation constellation)
        {
            foreach (var constellationPart in constellation.Parts)
            {
                if (TryGetBlockAtPosition(constellationPart.GetGridPosition(), out var blockOnMap))
                {
                    if (!blockOnMap.HasStar)
                    {
                        return false;
                    }

                    if (blockOnMap.HasStar && blockOnMap.BlockStar.IsAlreadyUsed)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryGetBlockAtPosition(Vector2Int gridPosition, out Block block)
        {
            foreach (var blockOnBoard in _blocksOnBoard)
            {
                if (blockOnBoard.GetGridPosition() == gridPosition)
                {
                    block = blockOnBoard;
                    return true;
                }
            }

            block = null;
            return false;
        }
    }
}