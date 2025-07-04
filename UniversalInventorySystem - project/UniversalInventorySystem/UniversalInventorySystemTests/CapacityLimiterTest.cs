using System.Collections.Generic;
using UniversalInventorySystemLibrary;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Serializer;

namespace UniversalInventorySystemTests
{
    [TestClass]
    public class CapacityLimiterTest
    {
        public static Inventory CreateInventoryWithCapacityLimiter(int capacity)
        {
            BaseItemContainer container = new BaseItemContainer();
            CapacityLimiter limiter = new CapacityLimiter(container, capacity);
            JsonSerializer serializer = new JsonSerializer();
            Inventory inventory = new Inventory(container, limiter, serializer);

            return inventory;
        }

        [TestMethod]
        public void TestCapacityLimiterAddItem()
        {
            Inventory inventory = CreateInventoryWithCapacityLimiter(5);

            for(int i = 0; i < 5; i++)
            {
                inventory.TryAdd(new BaseItem("item" + i));
            }

            Assert.IsTrue(inventory.TryAdd(new BaseItem("item" + 5)) == false);
        }

        [TestMethod]
        public void TestCapacityLimiterAddRangeItem()
        {
            Inventory inventory = CreateInventoryWithCapacityLimiter(5);

            for (int i = 0; i < 4; i++)
            {
                inventory.TryAdd(new BaseItem("item" + i));
            }

            IItem[] items = new IItem[2]
            {
                new BaseItem("item4"),
                new BaseItem("item5")
            };

            List<IItem> canAdd = new List<IItem>();
            List<IItem> cannotAdd = new List<IItem>();
            inventory.TryAddRange(items, canAdd, cannotAdd, false);

            Assert.AreEqual(cannotAdd.Count, 1);
            Assert.AreEqual((cannotAdd[0] as BaseItem).Name, "item5");
        }
    }
}
