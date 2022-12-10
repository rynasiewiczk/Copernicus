namespace _Project.Scripts
{
    using UnityEngine;

    public interface IDraggable
    {
        void SetWorldPosition(Vector3 position);
        void ResetRotation();
    }
}