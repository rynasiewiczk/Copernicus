namespace LazySloth.Pong.Gameplay
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public class CollisionCaller : MonoBehaviour
    {
        [SerializeField] private CollisionTag _collisionTag = CollisionTag.Default;
        [SerializeField] private CollisionProxy _proxy;

        private void OnTriggerEnter(Collider other)
        {
            _proxy.TriggerEnter(new TriggerInfo(other, _collisionTag));
        }

        private void OnTriggerExit(Collider other)
        {
            _proxy.TriggerExit(new TriggerInfo(other, _collisionTag));
        }

        private void OnTriggerStay(Collider other)
        {
            _proxy.TriggerStay(new TriggerInfo(other, _collisionTag));
        }
    }
}