namespace LazySloth.Observable
{
    using System;
    using System.Collections.Generic;

    public interface IReadOnlyObservablList<out T> : IEnumerable<T>
    {
        int Count { get; }
        T this[int index] { get; }

        IReadOnlyObservableProperty<int> CountObservable { get; }

        void SubscribeAdd(Action subscriber);
        void SubscribeAdd(Action<T> subscriber);
        void SubscribeRemove(Action subscriber);
        void SubscribeRemove(Action<T> subscriber);

        void UnsubscribeAdd(Action subscriber, bool optional = false);
        void UnsubscribeAdd(Action<T> subscriber, bool optional = false);
        void UnsubscribeRemove(Action subscriber, bool optional = false);
        void UnsubscribeRemove(Action<T> subscriber, bool optional = false);
    }
}