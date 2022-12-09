namespace LazySloth.Ui
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(GridLayoutGroup))]
    public class GridContentSizeFitter : MonoBehaviour, ILayoutSelfController
    {
        [SerializeField] private int _maxColumns = 1;
        
        private GridLayoutGroup _gridLayout_dontUse;
        private GridLayoutGroup GridLayout => _gridLayout_dontUse ??= GetComponent<GridLayoutGroup>();
        
        public void SetLayoutHorizontal()
        {
            RefreshContentSize();
        }

        public void SetLayoutVertical()
        {
            RefreshContentSize();
        }

        [ShowInInspector]
        private void RefreshContentSize()
        {
            if (GridLayout == null)
            {
                Debug.LogError("No gridLayout on gameObject!");
                return;
            }

            var rectTransform = transform as RectTransform;
            if(rectTransform == null) { return; }

            var childCount = 0;
            foreach (Transform child in transform)
            {
                var layoutElement = child.GetComponent<LayoutElement>();
                if (layoutElement != null && layoutElement.ignoreLayout)
                {
                    continue;
                }
                
                if (child.gameObject.activeSelf)
                {
                    childCount++;
                }
            }
            
            var rowsCount = (childCount / _maxColumns) + (childCount%_maxColumns == 0 ? 0 : 1);
            var columnsCount = rowsCount == 1 ? childCount : _maxColumns;

            var width = columnsCount * GridLayout.cellSize.x + GridLayout.padding.left + GridLayout.padding.right + GridLayout.spacing.x * (columnsCount - 1);
            var height = rowsCount * GridLayout.cellSize.y + GridLayout.padding.bottom + GridLayout.padding.top + GridLayout.spacing.y * (rowsCount - 1);

            rectTransform.sizeDelta = new Vector2(width, height);
        }
    } 
}
