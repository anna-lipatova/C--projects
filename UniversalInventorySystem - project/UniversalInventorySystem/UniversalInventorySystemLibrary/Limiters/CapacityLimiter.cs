using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Limiters
{
    /// <summary>
    /// Represents a limiter that restricts the number of items in a container based on capacity.
    /// </summary>
    public class CapacityLimiter: ContainerLimiter
    {
        private int _capacity;
        private int Capacity => _capacity;
        public CapacityLimiter(IItemContainer itemContainer, int capacity) : base(itemContainer)
        {
            _capacity = capacity;
        }

        public override bool CanAddItem(IItem item) => _itemContainer.Count < Capacity;

        public override bool CanAddItemArray(ICollection<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems)
        {
            foreach (var item in items)
            {
                if(CanAddItem(item))
                {
                    canAddItems.Add(item);
                }
                else
                {
                    cannotAddItems.Add(item);
                }
            }

            return cannotAddItems.Count == 0;
        }

        public override string ToString()
        {
            return $"{nameof(CapacityLimiter)}, Capacity = {Capacity}";
        }
    }
}
