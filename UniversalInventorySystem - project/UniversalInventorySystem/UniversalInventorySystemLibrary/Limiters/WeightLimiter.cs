using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Limiters
{
    public class WeightLimiter: ContainerLimiter
    {
        private float _maxWeight;
        
        public WeightLimiter(IItemContainer itemContainer, float capacity) : base(itemContainer)
        {
            _maxWeight = capacity;
        }

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
                    result += weight.GetWeight();
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
                if(currentWeight + itemWithWeight.GetWeight() <= _maxWeight)
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
                    float itemWeight = itemWithWeight.GetWeight();
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
    }
}
