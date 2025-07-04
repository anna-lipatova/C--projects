using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Container
{
    public interface IItemContainer : ICollection<IItem>
    {
        void AddRange(IEnumerable<IItem> newItems);
        List<IItem> GetItems();
    }
}
