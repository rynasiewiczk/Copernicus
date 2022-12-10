namespace _Project.Scripts.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constellations;
    using UnityEngine;

    public class LexiconConstellationsListController : MonoBehaviour
    {
        [SerializeField] private ConstellationsCatalog _constellationsCatalog;
        [SerializeField] private LexiconConstellationsDetailsController _detailsController;
        
        [SerializeField] private LexiconConstellationListItem _itemPrefab;
        [SerializeField] private RectTransform _itemsContainer;

        private List<LexiconConstellationListItem> _items = new();

        private void Awake()
        {
            foreach (var constellation in _constellationsCatalog.Constellations)
            {
                var item = Instantiate(_itemPrefab, _itemsContainer);
                item.Setup(constellation, HandleItemClick);
                _items.Add(item);
            }
        }

        private void OnEnable()
        {
            HandleItemClick(_items.First().Constellation);
        }

        private void HandleItemClick(Constellation constellation)
        {
            var isUnlocked = GameController.Instance.DroppedConstellations.Any(x => x.Name == constellation.Name);

            //if (isUnlocked)
            {
                _detailsController.Setup(constellation);
            }
        }
    }
}