using System;
using System.Collections.Generic;
using System.Collections;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Attributes;

namespace UniversalInventorySystemLibrary.Container
{
    /// <summary>
    /// Represents a base container for storing items.
    /// </summary>
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

        /// <summary>
        /// Add a range of items to the container.
        /// </summary>
        /// <param name="newItems"></param>
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

        /// <summary>
        /// Returns a list of items contained in the container.
        /// </summary>
        /// <returns>A list of items in the container.</returns>
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
        { 
            return GetEnumerator(); 
        }

        public override string ToString()
        {
            string result = string.Empty;
            foreach (IItem item in items)
            {
                result = "{";
                result += item.GetType();
                var properties = ItemUtils.GetItemPropertiesInfo(item);
                foreach (var property in properties)
                {
                    result += $", {property.Name} = {property.Value}";
                }
                result += "},";
            }

            return result;
        }
    }

    public class ItemContainerView
    {
        public void Show() { }
    }
}
