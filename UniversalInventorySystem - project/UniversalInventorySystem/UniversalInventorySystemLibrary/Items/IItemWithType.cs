using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalInventorySystemLibrary.Items
{
    public interface IItemWithType<EnumT> : IItem where EnumT : Enum
    {
        EnumT GetItemType();
    }
}
