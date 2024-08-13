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
    /// Represents a limiter that restricts the number of items of specific types 
    /// in a container based on predefined limits.
    /// </summary>
    /// <typeparam name="EnumT">The enum type used to define item types.</typeparam>
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

            int currentCount = GetTotalCountItemWithType(itemWithType.ItemType);
            if(currentCount < _limits[itemWithType.ItemType])
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

                EnumT type = itemWithType.ItemType;
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

                EnumT type = itemWithType.ItemType;
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

        /// <summary>
        /// Gets the total count of items of a specific type in the item container.
        /// </summary>
        /// <param name="type">The item type to count.</param>
        /// <returns>The total count of items of the specified type.</returns>
        public int GetTotalCountItemWithType(EnumT type)
        {
            int result = 0;
            foreach (var item in _itemContainer.GetItems())
            {
                IItemWithType<EnumT> itemWithType = item as IItemWithType<EnumT>;
                if (itemWithType == null)
                    continue;

                if (type.Equals(itemWithType.ItemType))
                    result++;
            }

            return result;
        }

        /// <summary>
        /// Gets the total count of items for each type in the provided dictionary.
        /// </summary>
        /// <param name="result">The dictionary to store the total counts of each item type.</param>
        public void GetTotalCountItemWithTypes(Dictionary<EnumT, int> result)
        {
            foreach (var item in _itemContainer.GetItems())
            {
                IItemWithType<EnumT> itemWithType = item as IItemWithType<EnumT>;
                if (itemWithType == null)
                    continue;

                EnumT type = itemWithType.ItemType;
                if (result.ContainsKey(type))
                {
                    result[type]++;
                }
            }
        }

        public override string ToString()
        {
            string result = $"{nameof(TypeLimiter<EnumT>)}, Limits: ";
            foreach (var limit in _limits)
            {
                result += $"[{limit.Key}] = {limit.Value},";
            }

            result.Remove(result.Length - 1, 1);

            return result;
        }
    }
}
