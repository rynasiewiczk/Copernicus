using UnityEngine;

namespace LazySloth.Observable
{
    using System;
    using System.Collections.Generic;
    using Sirenix.Serialization;
    using Utilities.Generics;

    [Serializable]
    public class ObservableProperty<T> : IObservableProperty<T>
    {
        private static readonly IEqualityComparer<T> DefaultEqualityComparer = UnityEqualityComparer.GetDefault<T>();

        private List<Action> _planeSubscribersDontUse = new List<Action>();
        private List<Action<T>> _subscribersDontUse = new List<Action<T>>();
        private List<Action<T, T>> _subscribersWithPreviousDontUse = new List<Action<T, T>>();

        private List<Action> PlaneSubscribers => _planeSubscribersDontUse ??= new List<Action>();
        private List<Action<T>> Subscribers => _subscribersDontUse ??= new List<Action<T>>();
        private List<Action<T, T>> SubscribersWithPrevious => _subscribersWithPreviousDontUse ??= new List<Action<T, T>>();

        [OdinSerialize] private T _value;

        public T PreviousValue { get; private set; }

        public T Value
        {
            get => _value;
            set
            {
                if (!DefaultEqualityComparer.Equals(_value, value))
                {
                    PreviousValue = _value;
                    _value = value;
                    NotifySubscribers();
                }
            }
        }

        public ObservableProperty()
        {
            _value = default;
        }

        public ObservableProperty(T value)
        {
            _value = value;
        }

        public void Subscribe(Action subscriber, bool withCall = false)
        {
            PlaneSubscribers.Add(subscriber);
            if (withCall)
            {
                subscriber?.Invoke();
            }
        }

        public void Subscribe(Action<T> subscriber, bool withCall = false)
        {
            Subscribers.Add(subscriber);
            if (withCall)
            {
                subscriber?.Invoke(Value);
            }
        }

        public void Subscribe(Action<T, T> subscriber, bool withCall = false)
        {
            SubscribersWithPrevious.Add(subscriber);
            if (withCall)
            {
                subscriber?.Invoke(Value, PreviousValue);
            }
        }

        public void Unsubscribe(Action subscriber, bool optional = false)
        {
            if (!optional && !PlaneSubscribers.Contains(subscriber))
            {
                throw new ArgumentException("Given method to unsubscribe was not found on subscriber list! (Did you use lambda expression to subscribe?)");
            }

            PlaneSubscribers.Remove(subscriber);
        }


        public void Unsubscribe(Action<T> subscriber, bool optional = false)
        {
            if (!optional && !Subscribers.Contains(subscriber))
            {
                throw new ArgumentException("Given method to unsubscribe was not found on subscriber list! (Did you use lambda expression to subscribe?)");
            }

            if (optional && !Subscribers.Contains(subscriber))
            {
                return;
            }

            Subscribers.Remove(subscriber);
        }

        public void Unsubscribe(Action<T, T> subscriber)
        {
            if (!SubscribersWithPrevious.Contains(subscriber))
            {
                throw new ArgumentException("Given method to unsubscribe was not found on subscriber list! (Did you use lambda expression to subscribe?)");
            }

            SubscribersWithPrevious.Remove(subscriber);
        }

        public void SetSilent(T value)
        {
            _value = value;
        }

        public void ForceNotifySubscribers()
        {
            NotifySubscribers();
        }

        private void NotifySubscribers()
        {
            for (int i = 0; i < Subscribers.Count; i++)
            {
                Subscribers[i]?.Invoke(Value);
            }

            for (int i = 0; i < PlaneSubscribers.Count; i++)
            {
                PlaneSubscribers[i]?.Invoke();
            }

            for (int i = 0; i < SubscribersWithPrevious.Count; i++)
            {
                SubscribersWithPrevious[i]?.Invoke(Value, PreviousValue);
            }
        }

        public override string ToString() => $"Value = {Value}";

        public void Dispose()
        {
            PlaneSubscribers.Clear();
            SubscribersWithPrevious.Clear();
            Subscribers.Clear();
        }
    }
}