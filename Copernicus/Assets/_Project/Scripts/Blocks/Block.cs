namespace _Project.Scripts
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Block : MonoBehaviour
    {
        [SerializeField] private GameObject _starContainer;
        [SerializeField] private BlockStar _blockStarPrefab;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _highlightRenderer;
        
        public BlockStar BlockStar { get; private set; }

        public bool IsOnMap { get; private set; }
        public bool HasStar { get; private set; }

        private Group _group;

        public void Init(Sprite sprite, bool hasStar, Group group)
        {
            Highlight(false);
            _group = group;
            _spriteRenderer.sprite = sprite;
            HasStar = hasStar;
            if (hasStar)
            {
                BlockStar = Instantiate(_blockStarPrefab, _starContainer.transform, false);
            }
        }

        public void Destroy()
        {
            _group.RemoveBlock(this);
            Destroy(gameObject);
        }

        public void Highlight(bool highlight)
        {
            _highlightRenderer.gameObject.SetActive(highlight);
        }

        public Vector2Int GetGridPosition()
        {
            var x = Mathf.RoundToInt(transform.position.x);
            var y = Mathf.RoundToInt(transform.position.y);
            return new Vector2Int(x, y);
        }
        
        public Vector2Int GetLocalGridPosition()
        {
            var x = Mathf.RoundToInt(transform.localPosition.x);
            var y = Mathf.RoundToInt(transform.localPosition.y);
            return new Vector2Int(x, y);
        }

        public void DropOnMap(float duration)
        {
            var gridPosition = GetGridPosition();

            if (HasStar)
            {
                BlockStar.MarkDroppedOnBoard();
            }

            transform.DOMove(new Vector2(gridPosition.x, gridPosition.y), duration);
            IsOnMap = true;
        }

        public void SetColor(Color color) => _spriteRenderer.color = color;

        public void Hide()
        {
            _spriteRenderer.DOFade(0.2f, 1f);
        }
        

        private void Update()
        {
            transform.rotation = Quaternion.identity;
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