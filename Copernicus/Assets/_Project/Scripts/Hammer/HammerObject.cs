namespace _Project.Scripts.Hammer
{
    using LazySloth.Observable;
    using UnityEngine;

    public class HammerObject : MonoBehaviour, IDraggable
    {
        public Transform Root => transform;
        public ObservableProperty<bool> IsDragged { get; } = new();

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
        
        public Vector2Int GetGridPosition()
        {
            var x = Mathf.RoundToInt(transform.position.x);
            var y = Mathf.RoundToInt(transform.position.y);
            return new Vector2Int(x, y);
        }
    }
}