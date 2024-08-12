using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Serializer;
using UniversalInventorySystemLibrary.Items;
using static UniversalInventorySystemLibrary.Container.Filterer;

namespace UniversalInventorySystemLibrary
{
    /// <summary>
    /// Represents an inventory thet can hold and manage items.
    /// </summary>
    public class Inventory
    {
        private IItemContainer container;
        private IContainerLimiter containerLimiter;
        private ISerializer serializer;

        public Inventory(IItemContainer container, IContainerLimiter containerLimiter, ISerializer serializer)
        {
            this.container = container;
            this.containerLimiter = containerLimiter;
            this.serializer = serializer;
        }

        /// <summary>
        /// Tries to add an item to the inventory.
        /// </summary>
        /// <param name="item">The item to add an item to the inventory.</param>
        /// <returns>Returns True if the item was added; otherwise returns False.</returns>
        public bool TryAdd(IItem item)
        {
            if (containerLimiter.CanAddItem(item))
            {
                container.Add(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to add a range of items to the inventory.
        /// </summary>
        /// <param name="newItems">The items to add.</param>
        /// <param name="canAddItems">The list to store items that can be added.</param>
        /// <param name="cannotAddItems">The list to store items that cannot be added.</param>
        /// <param name="allOnly">If True, only adds items if all can be added; otherwise, adds as many items as possible.</param>
        /// <returns>Returns True if all items were added; otherwise, returns False</returns>
        public bool TryAddRange(ICollection<IItem> newItems, List<IItem> canAddItems, List<IItem> cannotAddItems, bool allOnly = true)
        {
            bool canAdd = containerLimiter.CanAddItemArray(newItems, canAddItems, cannotAddItems);
            if
                (canAdd)
            {
                container.AddRange(newItems);
                return true;
            }
            else
            {
                if(allOnly = false)
                {
                    container.AddRange(canAddItems);
                }
                return false;
            }
        }

        /// <summary>
        /// Tries to add a range of items to the inventory.
        /// </summary>
        /// <param name="newItems">The items to add.</param>
        /// <param name="allOnly">If True, only adds items if all can be added; 
        /// otherwise, adds as many items as possible.
        /// </param>
        /// <returns>Returns True if all items were added; otherwise, returns False.</returns>
        public bool TryAddRange(ICollection<IItem> newItems, bool allOnly = true)
        {
            return TryAddRange(newItems, new List<IItem>(), new List<IItem>(), allOnly);
        }

        /// <summary>
        /// Serializes the inventory to a string.
        /// </summary>
        /// <returns>A string representation of the inventory.</returns>
        public string Serialize() => serializer.Serialize(this);

        /// <summary>
        /// Asynchronously serializes the inventory to a string.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a string representation of the inventory.
        /// </returns>
        public async Task<string> SerializeAsync() => await serializer.SerializeAsync(this);

        /// <summary>
        /// Copies properties from another inventory instance.
        /// </summary>
        /// <param name="source">The source inventory to copy properties from.</param>
        private void CopyPropertyFrom(Inventory source)
        {
            Type type = typeof(Inventory);
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] properties = type.GetProperties(flags);

            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var propertyValue = property.GetValue(source, null);
                    property.SetValue(source, propertyValue, null);
                }
            }

            source = null;
            
        }

        /// <summary>
        /// Deserializes a string to restore the inventory state.
        /// </summary>
        /// <param name="value">The string representation of the inventory.</param>
        public async void Deserialize(string value)
        {
            Inventory source = serializer.Deserialize(value);
            CopyPropertyFrom(source);
        }

        /// <summary>
        /// Asynchronously deserializes a string to restore the inventory state.
        /// </summary>
        /// <param name="value">The string representation of the inventory.</param>
        public async void DeserializeAsync(string value)
        {
            Inventory source = await serializer.DeserializeAsync(value);
            CopyPropertyFrom(source);
        }

        /// <summary>
        /// Sorts the items in the inventory by their name.
        /// </summary>
        public void SortByName() => QuickSorter.Sort<BaseItem>(container.GetItems(), "Name");

        /// <summary>
        /// Asynchronously sorts the items in the inventory by their name.
        /// </summary>
        public async void SortByNameAsync() => await QuickSorter.SortAsync<BaseItem>(container.GetItems(), "Name");

        /// <summary>
        /// Sorts the items in the inventory by a specified property.
        /// </summary>
        /// <typeparam name="T">The type of the items in the inventory.</typeparam>
        /// <param name="propertyName">The name of the property to sort by.</param>
        public void Sort<T>(string propertyName) where T : class
        {
            QuickSorter.Sort<T>(container.GetItems(), propertyName);
        }

        /// <summary>
        /// Asynchronously sorts the items in the inventory by a specified property.
        /// </summary>
        /// <typeparam name="T">The type of the items in the inventory.</typeparam>
        /// <param name="propertyName">The name of the property to sort by.</param>
        public async void SortAsync<T>(string propertyName) where T : class
        {
            await QuickSorter.SortAsync<T>(container.GetItems(), propertyName);
        }

        /// <summary>
        /// Filters the items in the inventory based on a specified field and comparison type.
        /// </summary>
        /// <typeparam name="T">The type of the items in the inventory.</typeparam>
        /// <param name="fieldName">The name of the field to filter by.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="comparisonType">The type of comparison to perform.</param>
        /// <returns>A list of items that match the filter criteria.</returns>
        public List<T> Filter<T>(string fieldName, object value, ComparisonType comparisonType) where T : class
        {
            return Filterer.Filter<T>(container.GetItems(), fieldName, value, comparisonType);
        }

        /// <summary>
        /// Asynchronously filters the items in the inventory based on a specified field and comparison type.
        /// </summary>
        /// <typeparam name="T">The type of the items in the inventory.</typeparam>
        /// <param name="fieldName">The name of the field to filter by.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="comparisonType">The type of comparison to perform.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a list of items that match the filter criteria.
        /// </returns>
        public async Task<List<T>> FilterAsync<T>(string fieldName, object value, ComparisonType comparisonType) where T: class
        {
            return await Task.Run(() =>
                Filterer.Filter<T>(container.GetItems(), fieldName, value, comparisonType)
            );
        }

        /// <summary>
        /// Finds the items in the inventory based on a specified field and value.
        /// </summary>
        /// <typeparam name="T">The type of the items in the inventory.</typeparam>
        /// <param name="fieldName">The name of the field to search by.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a list of items that match the search criteria.
        /// </returns>
        public List<T> Find<T>(string fieldName, object value) where T : class
        {
            return Finder.Find<T>(container.GetItems(), fieldName, value);
        }

        /// <summary>
        /// Asynchronously finds the items in the inventory based on a specified field and value.
        /// </summary>
        /// <typeparam name="T">The type of the items in the inventory.</typeparam>
        /// <param name="fieldName">The name of the field to search by.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a list of items that match the search criteria.
        /// </returns>
        public async Task<List<T>> FindAsync<T>(string fieldName, object value) where T: class
        {
            return await Task.Run(() =>
                Finder.Find<T>(container.GetItems(), fieldName, value)
            );
        }

        public override string ToString()
        {
            return $"Limiter: {containerLimiter}, Serializer: {serializer}, Items: {container}";
        }
    }
}
