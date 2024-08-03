using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Limiters
{
    public class CapacityLimiter: ContainerLimiter
    {
        public CapacityLimiter(IItemContainer itemContainer) : base(itemContainer)
        {
        }

        public override bool CanAddItem(IItem item)
        {
            throw new NotImplementedException();
        }

        public override bool CanAddItemArray(IEnumerable<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems)
        {
            throw new NotImplementedException ();
        }
    }
}
