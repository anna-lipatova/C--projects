using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Container
{
    public static class QuickSorter
    {
        private static void Swap<T>(List<T> items, int indexA, int indexB) where T : class
        {
            T temp = items[indexA];
            items[indexA] = items[indexB];
            items[indexB] = temp;
        }

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

        public static void Sort<T>(List<IItem> items, string fieldName) where T: class
        {
            List<T> itemWithCorrectType = new List<T>();
            foreach (var item in items)
            {
                T itemT = item as T;
                if (itemT != null)
                {
                    itemWithCorrectType.Add(itemT);
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
        }

        public static async Task SortAsync<T>(List<IItem> items, string fieldName) where T: class
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

        }
    }
}
