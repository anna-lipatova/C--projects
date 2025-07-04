using System.Collections.Generic;
using UniversalInventorySystemLibrary;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Serializer;

namespace UniversalInventorySystemTests
{
    [TestClass]
    public class WeightLimiterTest
    {
        public class ItemWithWeight : BaseItem, IItemWithWeight
        {
            private float _weight;
            public float Weight => _weight;

            public ItemWithWeight(string name, float weight) : base(name)
            {
                _weight = weight;
            }
        }

        public static Inventory CreateInventoryWithWeightLimiter(float maxWeight)
        {
            BaseItemContainer container = new BaseItemContainer();
            WeightLimiter limiter = new WeightLimiter(container, maxWeight);
            JsonSerializer serializer = new JsonSerializer();
            Inventory inventory = new Inventory(container, limiter, serializer);

            return inventory;
        }

        [TestMethod]
        public void TetsWeightLimiterAddItem()
        {
            Inventory inventory = CreateInventoryWithWeightLimiter(6);

            for (int i = 0; i< 3; i++)
            {
                inventory.TryAdd(new ItemWithWeight("item" + i, 2));
            }

            Assert.IsTrue(inventory.TryAdd(new ItemWithWeight("item3", 2)) == false);
        }

        [TestMethod]
        public void TestWeightLimiterAddRangeItem()
        {
            Inventory inventory = CreateInventoryWithWeightLimiter(8);

            for(int i = 0; i < 3; i++)
            {
                inventory.TryAdd(new ItemWithWeight("item" + i, 2));
            }

            IItem[] items = new IItem[3]
            {
                new ItemWithWeight("item3", 1),
                new ItemWithWeight("item4", 1),
                new ItemWithWeight("item5", 1)
            };

            List<IItem> canAdd = new List<IItem>();
            List<IItem> cannotAdd = new List<IItem>();
            inventory.TryAddRange(items, canAdd, cannotAdd, false);

            Assert.AreEqual(cannotAdd.Count, 1);
            Assert.AreEqual((cannotAdd[0] as BaseItem).Name, "item5");
        }
    }
}
