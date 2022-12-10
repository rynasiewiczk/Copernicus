namespace _Project.Scripts.UI
{
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

        private List<Constellation> _viewedConstellations = new();

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
            var unseenConstellations = GetUnseenConstellations();
            _items.ForEach(x => x.SetNotificationActive(false));

            foreach (var item in _items)
            {
                var isUnlocked = GameController.Instance.DroppedConstellations.Any(x => x.Name == item.Constellation.Name);
                item.SetUnlocked(isUnlocked);
            }
            
            foreach (var constellation in unseenConstellations)
            {
                SetItemNotificationActive(constellation);
            }
            
            HandleItemClick(_items.First().Constellation);
        }

        private void SetItemNotificationActive(Constellation constellation)
        {
            var item = _items.Single(x => x.Constellation.Name == constellation.Name);
            item.SetNotificationActive(true);
        }

        private List<Constellation> GetUnseenConstellations()
        {
            var allCollected = GameController.Instance.DroppedConstellations;
            var unseen = allCollected.Where(x => _viewedConstellations.Count == 0 || _viewedConstellations.All(y => y.Name != x.Name));
            var constellations = unseen as Constellation[] ?? unseen.ToArray();
            _viewedConstellations.AddRange(constellations);
            return constellations.ToList();
        }

        private void HandleItemClick(Constellation constellation)
        {
            var item = _items.Single(x => x.Constellation == constellation);
            item.SetNotificationActive(false);
            var isUnlocked = GameController.Instance.DroppedConstellations.Any(x => x.Name == constellation.Name);
            _detailsController.Setup(constellation, isUnlocked);
        }
    }
}