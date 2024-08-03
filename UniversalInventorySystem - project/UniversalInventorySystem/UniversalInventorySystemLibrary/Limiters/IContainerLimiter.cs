using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Limiters
{
    public interface IContainerLimiter
    {
        bool CanAddItem(IItem item);
        bool CanAddItemArray(ICollection<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems);

    }

    public abstract class ContainerLimiter: IContainerLimiter
    {
        protected IItemContainer _itemContainer;

        public ContainerLimiter(IItemContainer itemContainer)
        {
            _itemContainer = itemContainer;
        }

        public abstract bool CanAddItem(IItem item);
        public abstract bool CanAddItemArray(ICollection<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems);
    }
}
