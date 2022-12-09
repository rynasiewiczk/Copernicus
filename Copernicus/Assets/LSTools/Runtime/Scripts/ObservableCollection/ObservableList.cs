namespace LazySloth.Observable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Sirenix.Serialization;

    [Serializable]
    public class ObservableList<T> : IReadOnlyObservablList<T>, IDisposable
    {
        [NonSerialized] private List<Action> _planeAddSubscribers = new List<Action>();
        [NonSerialized] private List<Action<T>> _addSubscribers = new List<Action<T>>();

        [NonSerialized] private List<Action> _planeRemoveSubscribers = new List<Action>();
        [NonSerialized] private List<Action<T>> _removeSubscribers = new List<Action<T>>();

        [NonSerialized] private List<Action> _clearSubscribers = new List<Action>();
        [OdinSerialize] public List<T> List { get; private set; } = new List<T>();

        private IObservableProperty<int> _countObservable = new ObservableProperty<int>();

        public IReadOnlyObservableProperty<int> CountObservable => _countObservable;

        public int Count => List.Count;
        public bool IsReadOnly => false;

        public ObservableList()
        {
        }

        public ObservableList(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public void SetSilent(List<T> toSet)
        {
            List = toSet;
            _countObservable.SetSilent(List.Count);
        }

        private void FireRemoved(T oldItem)
        {
            _removeSubscribers.ForEach(a => a.Invoke(oldItem));
            _planeRemoveSubscribers.ForEach(a => a.Invoke());
            _countObservable.Value = List.Count;
        }

        private void FireAdded(T item)
        {
            _addSubscribers.ForEach(a => a.Invoke(item));
            _planeAddSubscribers.ForEach(a => a.Invoke());
            _countObservable.Value = List.Count;
        }

        private void FireCleared()
        {
            _clearSubscribers.ForEach(a => a.Invoke());
            _countObservable.Value = List.Count;
        }

        public void Dispose()
        {
            _planeAddSubscribers.Clear();
            _planeRemoveSubscribers.Clear();
            _addSubscribers.Clear();
            _removeSubscribers.Clear();
            _clearSubscribers.Clear();
            _countObservable.Dispose();
        }

        public void SubscribeAdd(Action subscriber)
        {
            _planeAddSubscribers.Add(subscriber);
        }

        public void SubscribeAdd(Action<T> subscriber)
        {
            _addSubscribers.Add(subscriber);
        }

        public void SubscribeRemove(Action subscriber)
        {
            _planeRemoveSubscribers.Add(subscriber);
        }

        public void SubscribeRemove(Action<T> subscriber)
        {
            _removeSubscribers.Add(subscriber);
        }

        public void UnsubscribeAdd(Action subscriber, bool optional = false)
        {
            if (!optional && !_planeAddSubscribers.Contains(subscriber))
            {
                throw new ArgumentException("Given method to unsubscribe was not found on subscriber list! (Did you use lambda expression to subscribe?)");
            }

            if (optional && !_planeAddSubscribers.Contains(subscriber))
            {
                return;
            }
                        
            _planeAddSubscribers.Remove(subscriber);
        }

        public void UnsubscribeAdd(Action<T> subscriber, bool optional = false)
        {
            if (!optional && !_addSubscribers.Contains(subscriber))
            {
                throw new ArgumentException("Given method to unsubscribe was not found on subscriber list! (Did you use lambda expression to subscribe?)");
            }

            if (optional && !_addSubscribers.Contains(subscriber))
            {
                return;
            }
            
            _addSubscribers.Remove(subscriber);
        }

        public void UnsubscribeRemove(Action subscriber, bool optional = false)
        {
            if (!optional && !_planeRemoveSubscribers.Contains(subscriber))
            {
                throw new ArgumentException("Given method to unsubscribe was not found on subscriber list! (Did you use lambda expression to subscribe?)");
            }

            if (optional && !_planeRemoveSubscribers.Contains(subscriber))
            {
                return;
            }
            
            _planeRemoveSubscribers.Remove(subscriber);

        }

        public void UnsubscribeRemove(Action<T> subscriber, bool optional = false)
        {
            if (!optional && !_removeSubscribers.Contains(subscriber))
            {
                throw new ArgumentException("Given method to unsubscribe was not found on subscriber list! (Did you use lambda expression to subscribe?)");
            }

            if (optional && !_removeSubscribers.Contains(subscriber))
            {
                return;
            }
            
            _removeSubscribers.Remove(subscriber);
        }

        public void SubscribeReset(Action subscriber)
        {
            _clearSubscribers.Add(subscriber);
        }

        public void UnsubscribeReset(Action subscriber, bool optional = false)
        {
            if (!optional && !_clearSubscribers.Contains(subscriber))
            {
                throw new ArgumentException("Given method to unsubscribe was not found on subscriber list! (Did you use lambda expression to subscribe?)");
            }

            if (optional && !_clearSubscribers.Contains(subscriber))
            {
                return;
            }
            
            _clearSubscribers.Remove(subscriber);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            List.Add(item);
            FireAdded(item);
        }

        public void Clear()
        {
            List.Clear();
            FireCleared();
        }

        public bool Contains(T item)
        {
            return List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var output = List.Remove(item);
            FireRemoved(item);
            
            return output;
        }

        public int IndexOf(T item)
        {
            return List.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            List.Insert(index, item);
            FireAdded(item);
        }

        public void RemoveAt(int index)
        {
            var item = List[index];
            List.RemoveAt(index);
            FireRemoved(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            List.AddRange(collection);
        }

        public void RemoveAll(Predicate<T> predicate)
        {
            List.RemoveAll(predicate);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            List.InsertRange(index, collection);
        }

        public void RemoveRange(int index, int count)
        {
            List.RemoveRange(index, count);
        }

        public T this[int index]
        {
            get { return List[index]; }
            set
            {
                var oldValue = List[index];
                List[index] = value;
                FireRemoved(oldValue);
                FireAdded(value);
            }
        }
    }
}