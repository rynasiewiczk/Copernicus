namespace _Project.Scripts
{
    using UnityEngine;

    public interface IDraggable
    {
        Transform Root { get; }
        void SetWorldPosition(Vector3 position);
        void ResetRotation();
    }
}