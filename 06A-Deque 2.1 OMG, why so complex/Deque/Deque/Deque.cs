using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


namespace Deque
{
    public sealed class Deque<T> : IList, IList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items = new T[DefaultCapacity];
        private int _count;

        public Deque()
        {
            _count = 0;
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

        public void Add(T value)
        {
            if (_count == _items.Length)
                IncreaseCapacity();
            _items[_count++] = value;
        }

        //returns index of the added element (is the last one)
        int IList.Add(object? value)
        {
            if (value == null && default(T) != null)
                throw new ArgumentNullException(nameof(value));

            try
            {
                Add((T)value!);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException(null, nameof(value));
            }

            return Count - 1;
        }

        public void Clear()
        {
            _items = new T[4];
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

            for (int i = index; i < Count - 1; i++)
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
            T[] newArray = new T[_items.Length * 2];
            Array.Copy(_items, newArray, _count);
            _items = newArray;
        }

        public IEnumerator GetEnumerator()
        {
            
        }

    }

}
