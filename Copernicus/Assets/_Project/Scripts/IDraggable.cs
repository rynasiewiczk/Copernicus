namespace _Project.Scripts
{
    using LazySloth.Observable;
    using UnityEngine;

    public interface IDraggable
    {
        Transform Root { get; }
        ObservableProperty<bool> IsDragged { get; }
        void SetWorldPosition(Vector3 position);
        void ResetRotation();
        void SetParentAndScale(Transform parent, Vector3 scale);
    }
}