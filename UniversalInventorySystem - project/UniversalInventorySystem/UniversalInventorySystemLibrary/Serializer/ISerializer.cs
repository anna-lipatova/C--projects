using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Serializer
{
    public interface ISerializer
    {
        string Serialize(Inventory container);
        Task<string> SerializeAsync(Inventory container);
        Inventory Deserialize(string value);
        Task<Inventory> DeserializeAsync(string value);
    }
}
