using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemLibrary.Limiters
{
    public interface IItemWithType
    {
        int GetTypeID();
    }
    public class TypeLimiter<EnumT> : IContainerLimiter where EnumT : Enum
    {
        private Dictionary<EnumT, int> _limits;

        public TypeLimiter(Dictionary<EnumT, int> limits)
        {
            _limits = limits;
        }
    
        public bool CanAddItem(IItem item)
        {
            IItemWithType itemWithType = item as IItemWithType;
            if (itemWithType != null)
            {
                EnumT type = (EnumT)Enum.ToObject(typeof(EnumT), itemWithType.GetTypeID());
            }

            return false;
        }

        public bool CanAddItemArray(ICollection<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems)
        {
            throw new NotImplementedException();
        }
    }
}
