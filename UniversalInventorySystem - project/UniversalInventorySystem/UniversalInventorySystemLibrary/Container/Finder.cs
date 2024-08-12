using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Container
{
    /// <summary>
    /// Provides methods to find items in the inventory system.
    /// </summary>
    public class Finder
    {
        /// <summary>
        /// Finds items in the list based on the specified field and value.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list f items to search.</param>
        /// <param name="fieldName">The name of the field to search by.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>A list of items that match the specified critaria.</returns>
        /// <exception cref="ArgumentException">Thrown if the specified field does not exist on the type.</exception>
        public static List<T> Find<T>(List<IItem> items, string fieldName, object value) where T : class
        {
            List<T> itemWithCorrectType = new List<T>();
            foreach (var item in items)
            {
                T itemT = item as T;
                if(itemT != null)
                {
                    itemWithCorrectType.Add(itemT);
                }
            }

            if(itemWithCorrectType == null || string.IsNullOrEmpty(fieldName))
            {
                return new List<T>();
            }

            Type type = typeof(T);
            PropertyInfo propertyInfo = type.GetProperty(fieldName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Field '{fieldName}' does not exist on type '{type.Name}'.");
            }

            List<T> foundItems = new List<T>();
            foreach (var item in foundItems)
            {
                var itemValue = propertyInfo.GetValue(item);
                if (itemValue != null && itemValue.Equals(value))
                {
                    foundItems.Add(item);
                }
            }

            return foundItems;
        }

        /// <summary>
        /// Asynchronously finds items in the list based on the specified field and value.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list f items to search.</param>
        /// <param name="fieldName">The name of the field to search by.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>A task that represents the asynchronous operation, 
        /// containing a list of items that match the specified critaria.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<List<T>> FindAsync<T>(List<T> items, string fieldName, object value) where T: class
        {
            List<T> itemWithCorrectType = new List<T>();
            foreach (var item in items)
            {
                T itemT = item as T;
                if(itemT != null)
                {
                    itemWithCorrectType.Add(itemT);
                }
            }

            if(itemWithCorrectType == null || string.IsNullOrEmpty(fieldName))
            {
                return new List<T>();
            }

            return await Task.Run(() =>
            {
                Type type = typeof(T);
                PropertyInfo propertyInfo = type.GetProperty(fieldName);
                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Field '{fieldName}' does not exist on type '{type.Name}'.");
                }

                List<T> foundItems = new List<T>();
                foreach(var item in itemWithCorrectType)
                {
                    var itemValue = propertyInfo.GetValue(item);
                    if (itemValue != null && itemValue.Equals(value))
                    {
                        foundItems.Add(item);
                    }
                }

                return foundItems;
            });
        }
    }
}
