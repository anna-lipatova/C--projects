using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Serializer;
using UnityEngine;

namespace UniversalInventorySystemLibrary
{
    public class Inventory
    {
        private IItemContainer container;
        private IContainerLimiter containerLimiter;
        private ISerializer serializer;
        private MonoBehaviour coroutineStarter;

        public bool TryAdd(IItem item)
        {
            if (containerLimiter.CanAddItem(item))
            {
                container.Add(item);
                return true;
            }
            return false;
        }

        public bool TryAddRange(IEnumerable<IItem> newItems, List<IItem> canAddItems, List<IItem> cannotAddItems, bool allOnly = true)
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

        public bool TryAddRange(IEnumerable<IItem> newItems, bool allOnly = true)
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
        }

        public async void Deserialize(string value)
        {
            Inventory source = serializer.Deserialize(value);
            CopyP
        }
    }
}
