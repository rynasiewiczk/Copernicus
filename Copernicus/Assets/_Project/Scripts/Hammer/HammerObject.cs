namespace _Project.Scripts.Hammer
{
    using LazySloth.Observable;
    using UnityEngine;

    public class HammerObject : MonoBehaviour, IDraggable
    {
        [SerializeField] private GameObject _pointer;
        
        public Transform Root => transform;
        public ObservableProperty<bool> IsDragged { get; } = new();

        private Block _currentTarget;

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }
        
        public void SetParentAndScale(Transform parent, Vector3 scale)
        {
            transform.SetParent(parent);
            transform.localScale = scale;
        }
        
        public void ResetRotation()
        {
        }

        private void Update()
        {
            _pointer.SetActive(IsDragged.Value);
            
            if (!IsDragged.Value)
            {
                if (_currentTarget != null)
                {
                    _currentTarget.Highlight(false);
                    _currentTarget = null;
                }
                
                return;
            }

            if (BoardController.Instance.IsPositionValidForHammer(this))
            {
                BoardController.Instance.TryGetBlockAtPosition(GetGridPosition(), out var block);
                {
                    if (_currentTarget != block)
                    {
                        if(_currentTarget != null) _currentTarget.Highlight(false);
                        _currentTarget = block;
                        block.Highlight(true);
                    }
                }
            }
            else
            {
                if (_currentTarget != null)
                {
                    _currentTarget.Highlight(false);
                    _currentTarget = null;
                }
            }
        }
        
        public Vector2Int GetGridPosition()
        {
            var x = Mathf.RoundToInt(transform.position.x);
            var y = Mathf.RoundToInt(transform.position.y);
            return new Vector2Int(x, y);
        }
    }
}