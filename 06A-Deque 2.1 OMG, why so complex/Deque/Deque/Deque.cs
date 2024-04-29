using System;
using System.Collections;
using System.Collections.Generic;


namespace Deque
{
    public sealed class Deque<T> : IList
    {
        private object[] _items = new object[8];
        private int _count;

        public Deque()
        {
            _count = 0;
        }

        public object this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }


        public bool IsFixedSize
        {
            get
            {
                return true;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }


        //returns index of the added element (is the last one)
        public int Add(object value)
        {
            if (_count < _items.Length)
            {
                _items[_count] = value;
                _count++;

                return (_count - 1);
            }

            return -1;
        }

        public void Clear()
        {
            _count = 0;
        }

        public bool Contains(object value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(object value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, object value)
        {
            if ((_count + 1 <= _items.Length) && (index < Count) && (index >= 0))
            {
                _count++;

                for (int i = Count - 1; i > index; i--)
                {
                    _items[i] = _items[i - 1];
                }
                _items[index] = value;
            }
        }

        public void Remove(object value)
        {
            RemoveAt(IndexOf(value));
        }

        public void RemoveAt(int index)
        {
            if ((index >= 0) && (index < Count))
            {
                for (int i = index; i < Count - 1; i++)
                {
                    _items[i] = _items[i + 1];
                }
                _count--;
            }
        }

        public void CopyTo(Array array, int index)
        {
            for (int i = 0; i < Count; i++)
            {
                array.SetValue(_items[i], index++);
            }
        }

        public IEnumerator GetEnumerator()
        {
            
        }

    }

}
