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
    /// Provides methods for sorting items in the inventory system using QuickSort algorithm.
    /// </summary>
    public static class QuickSorter
    {
        /// <summary>
        /// Swaps the elements as the specified indicies.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="items">The list of items.</param>
        /// <param name="indexA">The index of the first element to swap.</param>
        /// <param name="indexB">The index of the second element to swap.</param
        private static void Swap<T>(List<T> items, int indexA, int indexB) where T : class
        {
            T temp = items[indexA];
            items[indexA] = items[indexB];
            items[indexB] = temp;
        }

        /// <summary>
        /// Partitions the list into two halvef and returns the pivot index.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list of items to partition.</param>
        /// <param name="left">The starting index of the range to sort.</param>
        /// <param name="right">The ending index of the range to sort.</param>
        /// <param name="propertyInfo">The property information to sort by.</param>
        /// <returns>The index of the pivot.</returns>
        private static int Partition<T>(List<T> items, int left, int right, PropertyInfo propertyInfo) where T: class
        {
            T pivot = items[right];
            object pivotValue = propertyInfo.GetValue(pivot);
            int i = left;

            for (int j = left; j < right; j++)
            {
                if (Comparer<object>.Default.Compare(propertyInfo.GetValue(items[j]), pivotValue) <= 0)
                {
                    Swap(items, i, j);
                    i++;
                }
            }

            Swap(items, i, right);

            return i;
        }

        /// <summary>
        /// Sorts the items in the specified range using the QuickSort algorithm.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list of items to sort.</param>
        /// <param name="left">The starting index of the range to sort.</param>
        /// <param name="right">The ending index of the range to sort.</param>
        /// <param name="propertyInfo">The property information to sort by.</param>
        private static void Sort<T>(List<T> items, int left, int right, PropertyInfo propertyInfo) where T : class
        {
            if(left >= right)
            {
                return;
            }

            int pivotIndex = Partition(items, left, right, propertyInfo);
            Sort(items, left, pivotIndex - 1, propertyInfo);
            Sort(items, pivotIndex + 1, right, propertyInfo);
        }

        /// <summary>
        /// Sorts the items in the list based on the specified field.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list of items to sort.</param>
        /// <param name="fieldName">The name of the field to sort by.</param>
        /// <exception cref="ArgumentException">Thrown if the specified field does not exist on the type.</exception>
        public static void Sort<T>(List<IItem> items, string fieldName) where T: class
        {
            List<IItem> itemWithCorrectType = new List<IItem>();
            List<IItem> itemWithIncorrectType = new List<IItem>();

            foreach (var item in items)
            {
                T itemT = item as T;
                if (itemT != null)
                {
                    itemWithCorrectType.Add(item);
                }
                else
                {
                    itemWithIncorrectType.Add(item);
                }
            }

            if (itemWithCorrectType == null || itemWithCorrectType.Count <= 1 || string.IsNullOrEmpty(fieldName))
            {
                return;
            }

            Type type = typeof(T);
            PropertyInfo propertyInfo = type.GetProperty(fieldName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Field '{fieldName}' does not exist on type '{type.Name}'.");
            }

            Sort(itemWithCorrectType, 0, itemWithCorrectType.Count - 1, propertyInfo);

            items.Clear();
            items.AddRange(itemWithCorrectType);
            items.AddRange(itemWithIncorrectType);
        }

        /// <summary>
        /// Asynchronously sorts the items in the list based on the specified field.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list ofitems to sort.</param>
        /// <param name="fieldName">The name of the field to sort by.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown if the specified field does not exist on the type.</exception>
        public static async Task SortAsync<T>(List<IItem> items, string fieldName) where T: class
        {
            List<IItem> itemWithCorrectType = new List<IItem>();
            List<IItem> itemWithIncorrectType = new List<IItem>();

            foreach (var item in items)
            {
                T itemT = item as T;
                if(itemT != null)
                {
                    itemWithCorrectType.Add(item);
                }
                else
                {
                    itemWithIncorrectType.Add(item);
                }
            }

            if(itemWithCorrectType == null || itemWithCorrectType.Count <= 1 || string.IsNullOrEmpty(fieldName))
            {
                return;
            }

            Type type = typeof(T);
            PropertyInfo propertyInfo = type.GetProperty(fieldName);
            if(propertyInfo == null)
            {
                throw new ArgumentException($"Field '{fieldName}' does not exist on type '{type.Name}'.");
            }

            await Task.Run(() =>
            {
                Sort(itemWithCorrectType, 0, itemWithCorrectType.Count - 1, propertyInfo);
            });

            items.Clear();
            items.AddRange(itemWithCorrectType);
            items.AddRange(itemWithIncorrectType);
        }
    }
}
