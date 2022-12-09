namespace LazySloth.Pong.Gameplay
{
    using System;
    using UnityEngine;

    public class CollisionProxy : MonoBehaviour
    {
        public event Action<TriggerInfo> OnTriggerEnter;
        public event Action<TriggerInfo> OnTriggerStay;
        public event Action<TriggerInfo> OnTriggerExit;

        public void TriggerEnter(TriggerInfo triggerData)
        {
            OnTriggerEnter?.Invoke(triggerData);
        }

        public void TriggerExit(TriggerInfo triggerData)
        {
            OnTriggerExit?.Invoke(triggerData);
        }

        public void TriggerStay(TriggerInfo triggerData)
        {
            OnTriggerStay?.Invoke(triggerData);
        }
    }
}