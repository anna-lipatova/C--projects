using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Limiters
{
    public interface IContainerLimiter
    {
        bool CanAddItem(IItem item);
        bool CanAddItemArray(IEnumerable<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems);

    }
}
