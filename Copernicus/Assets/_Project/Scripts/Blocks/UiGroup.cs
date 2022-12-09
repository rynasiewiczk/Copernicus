namespace _Project.Scripts
{
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;

    public class UiGroup : MonoBehaviour
    {
        [SerializeField] private Group _group;
        [SerializeField] private Button _button;

        [SerializeField] private List<UiBlock> _uiBlocks;

        
        [SerializeField] private int _offset = 25;
        
        public Group Group => _group;

        private RectTransform RectTransform => transform as RectTransform;

        private void TryPickUpGroup()
        {
            //blocksDragger.TryPickUp(_group);
        }

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
            var countX = _uiBlocks.Select(x => x.GetGridPosition().x).Distinct().Count();
            return countX;
        }

        private int GetColumnsCount()
        {
            var countY = _uiBlocks.Select(x => x.GetGridPosition().y).Distinct().Count();
            return countY;
        }
    }
}