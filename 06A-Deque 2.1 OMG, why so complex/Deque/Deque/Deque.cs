using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


namespace Deque
{
    public sealed class Deque<T> : IList, IList<T>, IReadOnlyList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items = new T[DefaultCapacity];
        private int _head;
        private int _tail;
        private int _count;
        private int _version;

        public Deque()
        {
            _count = 0;
            _head = -1;
            _tail = -1;
        }

        object? IList.this[int index]
        {
            get => _items[index];
            set
            {
                if (value == null && default(T) != null)
                    throw new ArgumentNullException(nameof(value));

                try
                {
                    this[index] = (T)value!;
                }
                catch (InvalidCastException)
                {
                    throw new ArgumentException(null, nameof(value));
                }
            }
        }

        public T this[int index]
        {
            get
            {
                if (index > _count)
                    throw new IndexOutOfRangeException();
                return (T)_items[index];
            }

            set
            {
                if (index > _count)
                    throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }

        public int Count => _count;

        bool IList.IsFixedSize => false;

        bool ICollection<T>.IsReadOnly => false;

        bool IList.IsReadOnly => false;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        public bool isFull()
        {
            if (_head == 0 && _tail == _count - 1) return true;
            if (_head == _tail + 1) return true;
            return false;
        }

        public bool isEmpty()
        {
            if (_head == -1) return true;
            return false;
        }

        public void Add(T item)
        {
            if (isFull())
                IncreaseCapacity();

            if(_head == -1)
            {
                _head = 0;
                _tail = 0;
                _items[_count] = item;
            }
            _tail = (_tail + 1) % _count;
            _items[_count++] = item;
        }

        //returns index of the added element (is the last one)
        int IList.Add(object? value)
        {
            if (value == null && default(T) != null)
                throw new ArgumentNullException(nameof(value));

            try
            {
                Add((T)value);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException(null, nameof(value));
            }

            return Count - 1;
        }

        public void Clear()
        {
            _items = new T[DefaultCapacity];
            _count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public bool Contains(object value)
        {
            return Contains((T)value);
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf(_items, item, 0, _count);
        }

        public int IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the bounds of the Deque.");
            if (_count == _items.Length)
                IncreaseCapacity();

            if (index < _count)
            {
                Array.Copy(_items, index, _items, index + 1, _count - index);
            }

            _items[index] = item;
            _count++;
        }

        public void Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the bounds of the Deque.");

            if (_count > 1)
            {
                Array.Copy(_items, index + 1, _items, index, _count - index - 1);
            }
            _count--;
            _items[_count] = default(T);
        }

        public void Remove(object value)
        {
            int index = IndexOf(value);
            if (index != -1)
                RemoveAt(index);
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_items, 0, array, arrayIndex, _count);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((T[])array, index);
        }

        private void IncreaseCapacity()
        {
            try
            {
                T[] newArray = new T[_items.Length * 2];
                Array.Copy(_items, newArray, _count);
                _items = newArray;
            }
            catch (OverflowException)
            {
                throw new OverflowException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_items, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly T[] _items;
            private int _index;
            private int _count;
            private T _current;

            public Enumerator(T[] items, int count)
            {
                _items = items;
                _index = -1;
                _count = count;
                _current = default(T);
            }

            public T Current => _current; //{ get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_count == 0 || _index + 1 >= _count)
                    return false;

                _index++;
                _current = _items[_index];
                return true;
            }

            public void Reset()
            {
                _index = -1;
                _current = default(T);
            }

            public void Dispose()
            {
            }

        }


        public class ReverseView : IList<T>, IReadOnlyList<T>
        {
            private Deque<T> _deque;

            public ReverseView(Deque<T> deque)
            {
                _deque = deque;
            }

            public T this[int index]
            {
                get => _deque[_deque.Count - 1 - index];
                set => _deque[_deque.Count - 1 - index] = value;
            }

            public int Count => _deque.Count;

            public bool IsReadOnly => false;

            public void Add(T item)
            {
                _deque.Insert(0, item);
            }

            public void Clear()
            {
                _deque.Clear();
            }

            public bool Contains(T item)
            {
                return _deque.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                for (int i = 0; i < Count; i++)
                {
                    array[arrayIndex + i] = this[i];
                }
            }

            public int IndexOf(T item)
            {
                for (int i = 0; i < _deque.Count; i++)
                {
                    if (Equals(_deque[_deque.Count - 1 - i], item))
                    {
                        return i;
                    }
                }
                return -1;
            }

            public void Insert(int index, T item) => _deque.Insert(_deque.Count - index, item);

            public bool Remove(T item)
            {
                int index = IndexOf(item);
                if (index != -1)
                {
                    _deque.RemoveAt(_deque.Count - 1 - index);
                    return true;
                }
                return false;
            }

            public void RemoveAt(int index) => _deque.RemoveAt(_deque.Count - 1 - index);

            public IEnumerator<T> GetEnumerator()
            {
                return new ReverseViewEnumerator(this._deque);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private class ReverseViewEnumerator : IEnumerator<T>
            {
                private readonly Deque<T> _deque;
                private int _position;

                public ReverseViewEnumerator(Deque<T> deque)
                {
                    _deque = deque;
                    _position = deque.Count;
                }

                public T Current => _deque[_position];

                object IEnumerator.Current => Current;

                public bool MoveNext()
                {
                    _position--;
                    return _position >= 0;
                }

                public void Reset()
                {
                    _position = _deque.Count;
                }

                public void Dispose()
                {
                }
            }

        }


    }

}
