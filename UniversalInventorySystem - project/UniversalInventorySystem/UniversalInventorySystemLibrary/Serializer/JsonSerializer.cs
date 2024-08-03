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
        public string Serialize(Inventory inventory)
        {
            return JsonConvert.SerializeObject(inventory);
        }

        public async Task<string> SerializeAsync(Inventory inventory)
        {
            return await Task.Run(() => Serialize(inventory));
        }

        public Inventory Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Inventory>(value);
        }

        public async Task<Inventory> DeserializeAsync(string value)
        {
            return await Task.Run(() => Deserialize(value));
        }
    }
}
