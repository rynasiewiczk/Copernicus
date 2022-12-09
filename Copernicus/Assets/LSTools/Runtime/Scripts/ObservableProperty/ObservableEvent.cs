namespace LazySloth.Observable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ObservableEvent : IObservableEvent
    {
        private List<Action> _subscribersDontUse = new List<Action>();
        private List<Action> Subscribers => _subscribersDontUse ??= new List<Action>();
        
        public void Subscribe(Action subscriber, bool withCall = false)
        {
            Subscribers.Add(subscriber);
            if (withCall)
            {
                subscriber?.Invoke();
            }
        }
        
        public void Unsubscribe(Action subscriber)
        {
            if (!Subscribers.Contains(subscriber))
            {
                return;
            }

            Subscribers.Remove(subscriber);
        }
        
        public void Fire()
        {
            for (int i = 0; i < Subscribers.Count; i++)
            {
                Subscribers[i]?.Invoke();
            }
        }
    }

    public class ObservableEvent<T> : IObservableEvent<T>
    {
        private List<Action> _planeSubscribersDontUse = new List<Action>();
        private List<Action<T>> _subscribersDontUse = new List<Action<T>>();
        
        private List<Action> PlaneSubscribers => _planeSubscribersDontUse ??= new List<Action>();
        private List<Action<T>> Subscribers => _subscribersDontUse ??= new List<Action<T>>();

        public void Subscribe(Action subscriber)
        {
            PlaneSubscribers.Add(subscriber);
        }
        
        public void Subscribe(Action<T> subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public void Unsubscribe(Action subscriber, bool optional = false)
        {
            if (optional && PlaneSubscribers.Contains(subscriber))
            {
                PlaneSubscribers.Remove(subscriber);
            }
            else
            {
                PlaneSubscribers.Remove(subscriber);
            }
                
        }
        
        public void Unsubscribe(Action<T> subscriber, bool optional = false)
        {
            if (optional && Subscribers.Contains(subscriber))
            {
                Subscribers.Remove(subscriber);
            }
            else
            {
                Subscribers.Remove(subscriber);
            }
                
        }

        public void Fire(T t)
        {
            for (var i = 0; i < PlaneSubscribers.Count; i++)
            {
                PlaneSubscribers[i].Invoke();
            }
            
            for (var i = 0; i < Subscribers.Count; i++)
            {
                Subscribers[i]?.Invoke(t);
            }
        }
    }

    public class ObservableEvent<T, T2> : IObservableEvent<T, T2>
    {
        private readonly List<Action<T, T2>> _subscribers = new List<Action<T, T2>>();

        public void Subscribe(Action<T, T2> subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(Action<T, T2> subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void Fire(T t, T2 t2)
        {
            for (var i = 0; i < _subscribers.Count; i++)
            {
                _subscribers[i]?.Invoke(t, t2);
            }
        }
    }
}