using System.Collections.Generic;
using UniversalInventorySystemLibrary;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Items;

namespace UniversalInventorySystemTests
{
    [TestClass]
    public class BaseTest
    {
        [TestMethod]
        public void TestAddItem()
        {
            Inventory inventory = CapacityLimiterTest.CreateInventoryWithCapacityLimiter(5);

            inventory.TryAdd(new BaseItem("item1"));
            inventory.TryAdd(new BaseItem("item2"));

            Assert.IsTrue(inventory.ItemsCount == 2);
        }

        [TestMethod]
        public void TestAddItemRange()
        {
            Inventory inventory = CapacityLimiterTest.CreateInventoryWithCapacityLimiter(5);

            IItem[] items = new IItem[2]
            {
                new BaseItem("item1"),
                new BaseItem("item2")
            };

            inventory.TryAddRange(items);

            Assert.AreEqual(inventory.ItemsCount, 2);
            Assert.AreEqual((inventory.Items[1] as BaseItem).Name, "item2");
        }

        [TestMethod]
        public void TestSort()
        {
            Inventory inventory = CapacityLimiterTest.CreateInventoryWithCapacityLimiter(5);

            IItem[] items = new IItem[3]
            {
                new BaseItem("item1"),
                new BaseItem("item3"),
                new BaseItem("item2")
            };

            inventory.TryAddRange(items);
            inventory.SortByName();

            Assert.AreEqual((inventory.Items[0] as BaseItem).Name, "item1");
            Assert.AreEqual((inventory.Items[1] as BaseItem).Name, "item2");
            Assert.AreEqual((inventory.Items[2] as BaseItem).Name, "item3");
        }

        [TestMethod]
        public void TestFilter()
        {
            Inventory inventory = CapacityLimiterTest.CreateInventoryWithCapacityLimiter(4);
            IItem[] items = new IItem[3]
            {
                new WeightLimiterTest.ItemWithWeight("item1", 8),
                new WeightLimiterTest.ItemWithWeight("item2", 2),
                new WeightLimiterTest.ItemWithWeight("item3", 5)
            };

            inventory.TryAddRange(items);

            var result = inventory.Filter<WeightLimiterTest.ItemWithWeight>("Weight", 5f, Filterer.ComparisonType.LessThan);

            Assert.AreEqual(result[0].Name, "item2");
        }

        [TestMethod]
        public void TestFind()
        {
            Inventory inventory = CapacityLimiterTest.CreateInventoryWithCapacityLimiter(5);

            IItem[] items = new IItem[2]
            {
                new BaseItem("item1"),
                new BaseItem("item2")
            };

            inventory.TryAddRange(items);

            var foundItems = inventory.Find<BaseItem>("Name", "item2");

            Assert.AreEqual(1, foundItems.Count);
            Assert.AreEqual("item2", foundItems[0].Name);
        }
    }
}