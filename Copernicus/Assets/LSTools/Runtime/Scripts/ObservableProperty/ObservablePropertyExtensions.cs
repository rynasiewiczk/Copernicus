namespace LazySloth.Observable
{
    public static class ObservablePropertyExtensions
    {
        public static IReadOnlyObservableProperty<T> AsNewLinkedObservable<T>(this IReadOnlyObservableProperty<T> observable)
        {
            var newObservable = new ObservableProperty<T>();
            observable.Subscribe(v => newObservable.Value = v, true);

            return newObservable;
        }
    }
}