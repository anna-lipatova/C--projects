using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Limiters
{
    public class TypeLimiter : IContainerLimiter
    {
        public bool CanAddItem(IItem item)
        {
            throw new NotImplementedException();
        }

        public bool CanAddItemArray(IEnumerable<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems)
        {
            throw new NotImplementedException();
        }
    }
}
