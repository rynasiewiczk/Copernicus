namespace _Project.Scripts
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class UiGroup : MonoBehaviour
    {
        private Group _group;

        [SerializeField] private List<UiBlock> _uiBlocks;

        [SerializeField] private int _offset = 25;

        public Group Group => _group;

        private RectTransform RectTransform => transform as RectTransform;

        [Button]
        public void SetupPosition()
        {
            _uiBlocks = GetComponentsInChildren<UiBlock>().ToList();

            var rows = GetRowsCount();
            var columns = GetColumnsCount();

            var offsetX = rows % 2 == 0 ? _offset : 0;
            var offsetY = columns % 2 == 0 ? _offset : 0;

            RectTransform.anchoredPosition = new Vector2(offsetX, offsetY);
        }

        private int GetRowsCount()
        {
            var countX = _uiBlocks.Where(x => x.IsActive).Select(x => x.GetGridPosition().x).Distinct().Count();
            return countX;
        }

        private int GetColumnsCount()
        {
            var countY = _uiBlocks.Where(x => x.IsActive).Select(x => x.GetGridPosition().y).Distinct().Count();
            return countY;
        }

        public void Initialize([CanBeNull] Group group)
        {
            _group = group;

            _uiBlocks.ForEach(x => x.SetActive(false));

            if (group == null)
            {
                return;
            }
            
            foreach (var groupBlock in group.Blocks)
            {
                var pos = groupBlock.GetGridPosition();
                var uiBlock = _uiBlocks.SingleOrDefault(x => x.GetGridPosition() == pos);
                if (uiBlock == null)
                {
                    Debug.LogError($"There should be uiBlock at pos {pos}");
                    continue;
                }

                uiBlock.SetActive(true);
                SetupPosition();
            }
        }
    }
}