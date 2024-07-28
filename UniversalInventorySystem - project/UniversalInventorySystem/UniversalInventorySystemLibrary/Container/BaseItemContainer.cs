using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UniversalInventorySystemLibrary.Limiters;

namespace UniversalInventorySystemLibrary.Container
{
    public class BaseItemContainer: IItemContainer
    {
        private List<IItem> items;
        private IContainerLimiter containerLimiter;
        private MonoBehaviour coroutineStarter;

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

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(items);
        }

        public async Task<string> SerializeToJsonAsync()
        {
            return await Task.Run(SerializeToJson);
        }

        public void DeserializeFromJson(string json)
        {
            items = JsonConvert.DeserializeObject<List<IItem>>(json);
        }

        public async void DeserializeFromJsonAsync(string json)
        {
            await Task.Run(() =>  DeserializeFromJson(json));
        }

        public string SerializeToXML()
        {
            throw new NotImplementedException();
        }

        public void DeserializeFromXML(string xml)
        {
            
        }
    }
}
