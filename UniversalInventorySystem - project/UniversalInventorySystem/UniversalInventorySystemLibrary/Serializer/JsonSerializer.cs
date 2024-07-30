using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Serializer
{
    public class JsonSerializer: ISerializer
    {
        public string Serialize(IItemContainer container)
        {
            return JsonConvert.SerializeObject(container.GetItems());
        }

        public async Task<string> SerializeAsync(IItemContainer container)
        {
            return await Task.Run(() => Serialize(container));
        }

        public List<IItem> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<List<IItem>>(value);
        }

        public async Task<List<IItem>> DeserializeAsync(string value)
        {
            return await Task.Run(() => Deserialize(value));
        }
    }
}
