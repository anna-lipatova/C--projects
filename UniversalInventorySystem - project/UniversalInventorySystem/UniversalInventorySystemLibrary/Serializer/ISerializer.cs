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
        string Serialize(IItemContainer container);
        Task<string> SerializeAsync(IItemContainer container);
        List<IItem> Deserialize(string value);
        Task<List<IItem>> DeserializeAsync(string value);
    }
}
