using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Container
{
    public class Filterer
    {
        /// <summary>
        /// Provides methods to filter items in the inventory system.
        /// </summary>
        public enum ComparisonType
        {
            Equal,
            GreaterThan,
            LessThan
        }

        /// <summary>
        /// Compares the values based on the specified comparison type.
        /// </summary>
        /// <param name="itemValue">The value of the item.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="comparisonType">The type of comparison to perform.</param>
        /// <returns>Returns True if the comparison is successful, False otherwise.</returns>
        private static bool CompareValues<T>(T itemValue, T value, ComparisonType comparisonType)
        {
            Type type = typeof(T);
            Type type1 = itemValue.GetType();
            Type type2 = value.GetType();

            int comperisonResult = Comparer<T>.Default.Compare(itemValue, value);

            switch (comparisonType)
            {
                case ComparisonType.Equal:
                    return comperisonResult == 0;
                case ComparisonType.GreaterThan:
                    return comperisonResult > 0;
                case ComparisonType.LessThan:
                    return comperisonResult < 0;
            };

            return false;
        }

        /// <summary>
        /// Filters the items in the list based on the specified field and comparison type.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list of items to filter.</param>
        /// <param name="fieldName">The Name of the field to filter by.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="comparisonType">The type of comparison to perform.</param>
        /// <returns>A list of items that match the specified criteria.</returns>
        /// <exception cref="ArgumentException">Throws if the specified field does not exist on the type.</exception>
        public static List<T> Filter<T>(List<IItem> items, string fieldName, object value, ComparisonType comparisonType) where T : class
        {
            List<T> itemWithCorrectType = new List<T>();
            foreach(var item in items)
            {
                T itemT = item as T;
                if (itemT != null)
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
            if(propertyInfo == null)
            {
                throw new ArgumentException($"Field '{fieldName}' does not esist on type '{type.Name}'.");
            }

            List<T> filteredItems = new List<T>();
            foreach(var item in itemWithCorrectType)
            {
                var itemValue = propertyInfo.GetValue(item);
                if (itemValue != null && CompareValues(itemValue, value, comparisonType))
                {
                    filteredItems.Add(item);
                }
            }

            return filteredItems;
        }


        /// <summary>
        /// Asynchronously filters the items in the list based on the specified field and comparison type.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list of items to filter.</param>
        /// <param name="fieldName">The Name of the field to filter by.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="comparisonType">The type of comparison to perform.</param>
        /// <returns>A task that represents the asynchronous operation, containing a list of items that match the specified criteria.</returns>
        /// <exception cref="ArgumentException">Throw if the specified field does not exist on the type.</exception>
        public static async Task<List<T>> FilterAsync<T>(List<IItem> items, string fieldName, object value, ComparisonType comparisonType) where T: class
        {
            List<T> itemWithCorrectType = new List<T>();
            foreach(var item in items)
            {
                var itemT = item as T;
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
                    throw new ArgumentException($"Field '{fieldName}' does not wxist on type '{type.Name}'.");
                }

                List<T> filteredItems = new List<T>();
                foreach(var item in itemWithCorrectType)
                {
                    var itemValue = propertyInfo.GetValue(item);
                    if(itemValue != null && CompareValues(itemValue, value, comparisonType))
                    {
                        filteredItems.Add(item);
                    }
                }

                return filteredItems;
            }
            );
        }
    }
}
