using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Limiters
{
    /// <summary>
    /// Represents an abstract base class for limiters that restrict the items in a container.
    /// </summary>
    public abstract class ContainerLimiter : IContainerLimiter
    {
        /// <summary>
        /// The item container that the limiter is applied to.
        /// </summary>
        protected IItemContainer _itemContainer;

        public ContainerLimiter(IItemContainer itemContainer)
        {
            _itemContainer = itemContainer;
        }

        public abstract bool CanAddItem(IItem item);
        public abstract bool CanAddItemArray(ICollection<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems);
    }
}
