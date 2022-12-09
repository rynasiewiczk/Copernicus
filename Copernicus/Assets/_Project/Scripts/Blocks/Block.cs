namespace _Project.Scripts
{
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Block : MonoBehaviour
    {
        [SerializeField] private GameObject _starContainer;
        [SerializeField] private BlockStar _blockStarPrefab;
        
        public BlockStar BlockStar { get; private set; }

        public bool IsOnMap { get; private set; }
        public bool HasStar { get; private set; }

        public void Init(bool hasStar)
        {
            HasStar = hasStar;
            if (hasStar)
            {
                BlockStar = Instantiate(_blockStarPrefab, _starContainer.transform, false);
            }
        }

        public Vector2Int GetGridPosition()
        {
            var x = Mathf.RoundToInt(transform.position.x);
            var y = Mathf.RoundToInt(transform.position.y);
            return new Vector2Int(x, y);
        }

        public void DropOnMap()
        {
            //spawn block (check for star) on a map with current grid position (new one or this one?)
        }

        [Button]
        public void Validate()
        {
            OnValidate();
        }
        
        private void OnValidate()
        {
            var group = GetComponentInParent<Group>();

            if (group != null)
            {
                var x = Mathf.RoundToInt(transform.localPosition.x);
                var y = Mathf.RoundToInt(transform.localPosition.y);
                gameObject.name = $"Block_{x}_{y}";
            }
        }
        
    }
}