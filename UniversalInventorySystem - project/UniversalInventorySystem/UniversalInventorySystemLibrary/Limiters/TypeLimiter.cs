using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Limiters
{
    public interface IItemWithType<EnumT> : IItem where EnumT: Enum
    {
        EnumT GetItemType();
    }

    public class TypeLimiter<EnumT> : ContainerLimiter where EnumT : Enum
    {
        private Dictionary<EnumT, int> _limits;

        public TypeLimiter(IItemContainer itemContainer, Dictionary<EnumT, int> limits): base(itemContainer) 
        {
            _limits = limits;
        }
    
        public override bool CanAddItem(IItem item)
        {
            IItemWithType<EnumT> itemWithType = item as IItemWithType<EnumT>;
            if (itemWithType == null)
            {
                throw new Exception("Incorrect item type.");
            }

            int currentCount = GetTotalCountItemWithType(itemWithType.GetItemType());
            if(currentCount < _limits[itemWithType.GetItemType()])
            {
                return true;
            }

            return false;
        }

        public override bool CanAddItemArray(ICollection<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems)
        {
            Dictionary<EnumT, int> countItemWithTypes = new Dictionary<EnumT, int>();
            foreach (var item in items)
            {
                IItemWithType<EnumT> itemWithType = item as IItemWithType<EnumT>;
                if (itemWithType == null)
                    continue;

                EnumT type = itemWithType.GetItemType();
                if(countItemWithTypes.ContainsKey(type) == false)
                {
                    countItemWithTypes.Add(type, 0);
                }
            }

            GetTotalCountItemWithTypes(countItemWithTypes);

            foreach(var item in items)
            {
                IItemWithType<EnumT> itemWithType = item as IItemWithType<EnumT>;
                if (itemWithType == null)
                    continue;

                EnumT type = itemWithType.GetItemType();
                if (countItemWithTypes[type] < _limits[type])
                {
                    countItemWithTypes[type]++;
                    canAddItems.Add(item);
                }
                else
                {
                    cannotAddItems.Add(item);
                }
            }

            return cannotAddItems.Count == 0;
        }

        public int GetTotalCountItemWithType(EnumT type)
        {
            int result = 0;
            foreach (var item in _itemContainer.GetItems())
            {
                IItemWithType<EnumT> itemWithType = item as IItemWithType<EnumT>;
                if (itemWithType == null)
                    continue;

                if (type.Equals(itemWithType.GetItemType()))
                    result++;
            }

            return result;
        }

        public void GetTotalCountItemWithTypes(Dictionary<EnumT, int> result)
        {
            foreach (var item in _itemContainer.GetItems())
            {
                IItemWithType<EnumT> itemWithType = item as IItemWithType<EnumT>;
                if (itemWithType == null)
                    continue;

                EnumT type = itemWithType.GetItemType();
                if (result.ContainsKey(type))
                {
                    result[type]++;
                }
            }
        }
    }
}
