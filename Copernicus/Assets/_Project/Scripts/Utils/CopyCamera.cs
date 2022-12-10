namespace _Project.Scripts.Utils
{
    using UnityEngine;

    public class CopyCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Camera _cameraToCopy;

        private void Update()
        {
            _camera.orthographicSize = _cameraToCopy.orthographicSize;
            _camera.transform.position = _cameraToCopy.transform.position;
        }
        
    }
}