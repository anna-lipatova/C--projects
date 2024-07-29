using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniversalInventorySystemLibrary.Limiters;
using System.ComponentModel;
using UniversalInventorySystemLibrary.Serializer;

namespace UniversalInventorySystemLibrary.Container
{
    public class BaseItemContainer: IItemContainer
    {
        private List<IItem> items;
        private IContainerLimiter containerLimiter;
        private MonoBehaviour coroutineStarter;
        private ISerializer serializer;

        public List<IItem> GetItems()
        {
            return items;
        }

        public bool TryAdd(IItem item)
        {
            if (containerLimiter.CanAddItem(item))
            {
                items.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryAddRange(IEnumerable<IItem> newItems, List<IItem> canAddItems, List<IItem> cannotAddItems, bool allOnly = true)
        {
            bool canAdd = containerLimiter.CanAddItemArray(newItems, canAddItems, cannotAddItems);
            if (canAdd)
            {
                items.AddRange(newItems);
                return true;
            }
            else
            {
                if(allOnly == false)
                {
                    items.AddRange(canAddItems);
                }
                return false;
            }
        }

        public string Serialize() => serializer.Serialize(this);

        public async Task<string> SerializeAsync() => await serializer.SerializeAsync(this);

        public void Deserialize(string value)
        {
            items = serializer.Deserialize(value);
        }

        public async void DeserializeAsync(string value)
        {
            items = await serializer.DeserializeAsync(value);
        }

    }
}
