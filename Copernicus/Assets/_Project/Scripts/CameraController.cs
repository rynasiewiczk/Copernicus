namespace _Project.Scripts
{
    using UnityEngine;

    public class CameraController : SingletonBehaviour<CameraController>
    {
        [SerializeField] private float _moveSpeed;
        
        private void Update()
        {
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

            transform.position += moveDir * _moveSpeed * Time.deltaTime;
        }
    }
}