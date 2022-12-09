namespace LazySloth.Pong.Gameplay
{
    using UnityEngine;

    public class TriggerInfo
    {
        public Collider Other { get; }
        public CollisionTag Tag { get; }

        public TriggerInfo(Collider other, CollisionTag tag)
        {
            Other = other;
            Tag = tag;
        }
    }
}