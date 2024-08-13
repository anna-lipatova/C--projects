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
    /// Represents a limiter thet restricts the total weight of items in a container.
    /// </summary>
    public class WeightLimiter: ContainerLimiter
    {
        private float _maxWeight;
        public float MaxWeight => _maxWeight;
        
        public WeightLimiter(IItemContainer itemContainer, float maxWeight) : base(itemContainer)
        {
            _maxWeight = maxWeight;
        }

        /// <summary>
        /// Gets the total weight of items currently in the item container. 
        /// </summary>
        /// <returns>The total weight of items in the container.</returns>
        public float GetTotalWeight()
        {
            float result = 0;
            foreach (var item in _itemContainer.GetItems())
            {
                IItemWithWeight weight = item as IItemWithWeight;
                if (weight == null)
                {
                    continue;
                }
                else
                {
                    result += weight.Weight;
                }
            }

            return result;
        }

        public override bool CanAddItem(IItem newItem)
        {
            IItemWithWeight itemWithWeight = newItem as IItemWithWeight;
            if (itemWithWeight == null)
            {
                throw new Exception("Incorrect item type.");
            }
            else
            {
                float currentWeight = GetTotalWeight();
                if(currentWeight + itemWithWeight.Weight <= _maxWeight)
                {
                    return true;
                }
            }

            return false;

        }

        public override bool CanAddItemArray(ICollection<IItem> items, List<IItem> canAddItems, List<IItem> cannotAddItems)
        {
            float currentWeight = GetTotalWeight();
            foreach(var newItem in items)
            {
                IItemWithWeight itemWithWeight= newItem as IItemWithWeight;
                if(itemWithWeight == null)
                {
                    return true;
                }
                else
                {
                    float itemWeight = itemWithWeight.Weight;
                    if (currentWeight + itemWeight <= _maxWeight)
                    {
                        currentWeight += itemWeight;
                        canAddItems.Add(newItem);
                    }
                    else
                    {
                        cannotAddItems.Add(newItem);
                    }
                }
            }

            return cannotAddItems.Count == 0;
        }

        public override string ToString()
        {
            return $"{nameof(WeightLimiter)}, Maximal Weight = {MaxWeight}";
        }
    }
}
