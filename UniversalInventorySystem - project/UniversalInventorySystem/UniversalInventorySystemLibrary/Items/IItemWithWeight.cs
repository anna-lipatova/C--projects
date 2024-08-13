using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalInventorySystemLibrary.Items
{
    public interface IItemWithWeight: IItem
    {
        float Weight { get; }
    }
}
