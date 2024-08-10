using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Serializer;
using UniversalInventorySystemLibrary.Items;
using static UniversalInventorySystemLibrary.Container.Filterer;

namespace UniversalInventorySystemLibrary
{
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

        public bool TryAdd(IItem item)
        {
            if (containerLimiter.CanAddItem(item))
            {
                container.Add(item);
                return true;
            }
            return false;
        }

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

        public bool TryAddRange(ICollection<IItem> newItems, bool allOnly = true)
        {
            return TryAddRange(newItems, new List<IItem>(), new List<IItem>(), allOnly);
        }

        public string Serialize() => serializer.Serialize(this);

        public async Task<string> SerializeAsync() => await serializer.SerializeAsync(this);

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

        public async void Deserialize(string value)
        {
            Inventory source = serializer.Deserialize(value);
            CopyPropertyFrom(source);
        }

        public async void DeserializeAsync(string value)
        {
            Inventory source = await serializer.DeserializeAsync(value);
            CopyPropertyFrom(source);
        }

        public void Sort() => QuickSorter.Sort<BaseItem>(container.GetItems(), "Name");

        public async void SortAsync() => await QuickSorter.SortAsync<BaseItem>(container.GetItems(), "Name");

        public void Sort<T>(string propertyName) where T : class
        {
            QuickSorter.Sort<T>(container.GetItems(), propertyName);
        }

        public async void  SortAsync<T>(string propertyName) where T : class
        {
            await QuickSorter.SortAsync<T>(container.GetItems(), propertyName);
        }

        public List<T> Filter<T>(string fieldName, object value, ComparisonType comparisonType) where T : class
        {
            return Filterer.Filter<T>(container.GetItems(), fieldName, value, comparisonType);
        }

        public async Task<List<T>> FilterAsync<T>(string fieldName, object value, ComparisonType comparisonType) where T: class
        {
            return await Task.Run(() =>
                Filterer.Filter<T>(container.GetItems(), fieldName, value, comparisonType)
            );
        }

        public override string ToString()
        {
            return $"Limiter: {containerLimiter}, Serializer: {serializer}, Items: {container}";
        }
    }
}
