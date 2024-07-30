using System;
using System.Collections.Generic;
using System.Collections;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Container
{

    public interface IItemContainer : ICollection<IItem>
    {
        void AddRange(IEnumerable<IItem> newItems);
        List<IItem> GetItems();

    }

    public class BaseItemContainer: IItemContainer
    {
        private List<IItem> items;

        public BaseItemContainer()
        {
           items = new List<IItem>();
        }

        //prop Count
        public int Count => items.Count;

        //prop IsReadOnly
        public bool IsReadOnly => false;

        //method add
        public void Add(IItem item) 
        {
            items.Add(item);
        }

        //method add range
        public void AddRange(IEnumerable<IItem> newItems)
        {
            items.AddRange(newItems);
        }

        //method clear
        public void Clear()
        {
            items.Clear();
        }

        //method Contains
        public bool Contains(IItem item)
        {
            return items.Contains(item);
        }

        //method CopyTo
        public void CopyTo(IItem[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        //method GetEnumerator
        public IEnumerator<IItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        //method GetItems
        public List<IItem> GetItems()
        {
            return new List<IItem>(items);
        }

        //method remove
        public bool Remove(IItem item)
        {
            return items.Remove(item);
        }

        //method IEnamerable.GetEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }

    }

    public class ItemContainerView
    {
        public void Show() { }
    }
}
