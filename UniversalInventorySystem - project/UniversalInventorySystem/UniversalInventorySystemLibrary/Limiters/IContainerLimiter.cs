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
        /// <summary>
        /// Determines whether a new item can be added to the item container.
        /// </summary>
        /// <param name="item">The new item to add.</param>
        /// <returns>Returns True if the item can be added; otherwise, returns False.</returns>
        bool CanAddItem(IItem item);

        /// <summary>
        /// Determines whether an array of items can be added to the item container.
        /// </summary>
        /// <param name="items">The collection of items to add.</param>
        /// <param name="canAddItems">The list to store items that can be added.</param>
        /// <param name="cannotAddItems">The list to store items that cannot be added.</param>
        /// <returns></returns>
        bool CanAddItemArray(ICollection<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems);

    }

    
}
