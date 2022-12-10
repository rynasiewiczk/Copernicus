namespace _Project.Scripts
{
    using Cinemachine;
    using DG.Tweening;
    using UnityEngine;

    public class CameraController : SingletonBehaviour<CameraController>
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _zoomSpeed;
        [SerializeField] private float _minZoom;
        [SerializeField] private float _maxZoom;

        private bool _isShowingEnd;
        
        private void Update()
        {
            if(_isShowingEnd) { return; }
            var moveDir = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                moveDir.y += 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                moveDir.y -= 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveDir.x -= 1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveDir.x += 1;
            }

            var ortoChange = 0f;
            if (Input.GetKey(KeyCode.Z))
            {
                ortoChange += 1f;
            }

            if (Input.GetKey(KeyCode.X))
            {
                ortoChange -= 1f;
            }
            
            transform.position += moveDir * _moveSpeed * Time.deltaTime;

            var newSize = _virtualCamera.m_Lens.OrthographicSize + ortoChange * _zoomSpeed * Time.deltaTime;
            newSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);
            _virtualCamera.m_Lens.OrthographicSize = newSize;
        }

        public void ShowEndSequence(Vector2 center, float minx, float miny)
        {
            _isShowingEnd = true;
            transform.position = center;

            var aspect = _virtualCamera.m_Lens.Aspect;
            
            var sizeForY = (miny + 2f) / 2f;
            var sizeForX = ((minx + 2f) / 2f) / aspect;
            var startSize = _virtualCamera.m_Lens.OrthographicSize;
            var newSize  = Mathf.Max(sizeForX, sizeForY);
            
            DOVirtual.Float(startSize, newSize, 2f, x => 
            {
                _virtualCamera.m_Lens.OrthographicSize = x;
            });
        }
    }
}