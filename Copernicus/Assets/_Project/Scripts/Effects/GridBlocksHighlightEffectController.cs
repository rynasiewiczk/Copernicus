namespace _Project.Scripts.Effects
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class GridBlocksHighlightEffectController : SingletonBehaviour<GridBlocksHighlightEffectController>
    {
        [SerializeField] private List<Transform> _items;

        public void Clear()
        {
            _items.ForEach(x => x.gameObject.SetActive(false));
        }

        public void SetForGroup(Group group)
        {
            Clear();

            foreach (var block in group.Blocks)
            {
                var gridPosition = block.GetGridPosition();
                var item = _items.First(x => !x.gameObject.activeSelf);
                item.gameObject.SetActive(true);
                item.transform.position = new Vector2(gridPosition.x, gridPosition.y);
            }
        }
    }
}