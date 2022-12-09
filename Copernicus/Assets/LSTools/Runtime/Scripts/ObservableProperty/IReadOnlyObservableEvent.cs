namespace LazySloth.Observable
{
    using System;

    public interface IReadOnlyObservableEvent
    {
        void Subscribe(Action subscriber, bool withCall = false);
        void Unsubscribe(Action subscriber);
    }

    public interface IReadOnlyObservableEvent<T>
    {
        void Subscribe(Action subscriber);
        void Unsubscribe(Action subscriber, bool optional = false);
        void Subscribe(Action<T> subscriber);
        void Unsubscribe(Action<T> subscriber, bool optional = false);
    }
    
    public interface IReadOnlyObservableEvent<T, T2>
    {
        void Subscribe(Action<T, T2> subscriber);
        void Unsubscribe(Action<T, T2> subscriber);
    }
}