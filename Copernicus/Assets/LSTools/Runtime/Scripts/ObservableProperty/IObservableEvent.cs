namespace LazySloth.Observable
{
    public interface IObservableEvent : IReadOnlyObservableEvent
    {
        void Fire();
    }

    public interface IObservableEvent<T> : IReadOnlyObservableEvent<T>
    {
        void Fire(T t);
    }
    
    public interface IObservableEvent<T, T2> : IReadOnlyObservableEvent<T, T2>
    {
        void Fire(T t, T2 t2);
    }
}