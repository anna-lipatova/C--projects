using System.Collections.Generic;
using UniversalInventorySystemLibrary;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Serializer;

namespace UniversalInventorySystemTests
{
    [TestClass]
    public class TypeLimiterTest
    {
        public enum ItemType
        {
            Tool,
            Plant
        }

        public class ItemWithType : BaseItem, IItemWithType<ItemType>
        {
            private ItemType _itemType;
            public ItemType ItemType => _itemType;

            public ItemWithType(string name, ItemType itemType) : base(name)
            {
                _itemType = itemType;
            }
        }

        public static Inventory CreateInventoryWithTypeLimiter(Dictionary<ItemType, int> limits)
        {
            BaseItemContainer container = new BaseItemContainer();
            TypeLimiter<ItemType> limiter = new TypeLimiter<ItemType>(container, limits);
            JsonSerializer serializer = new JsonSerializer();
            Inventory inventory = new Inventory(container, limiter, serializer);

            return inventory;
        }

        [TestMethod]
        public void TestTypeLimiterAddItem()
        {
            Dictionary<ItemType, int> limits = new Dictionary<ItemType, int>()
            {
                [ItemType.Tool] = 1,
                [ItemType.Plant] = 2
            };

            Inventory inventory = CreateInventoryWithTypeLimiter(limits);
            inventory.TryAdd(new ItemWithType("item1", ItemType.Tool));

            Assert.AreEqual(inventory.TryAdd(new ItemWithType("item2", ItemType.Tool)), false);
            Assert.AreEqual(inventory.TryAdd(new ItemWithType("item3", ItemType.Plant)), true);
        }

        [TestMethod]
        public void TestTypeLimiterAddRangeItem()
        {
            Dictionary<ItemType, int> limits = new Dictionary<ItemType, int>()
            {
                [ItemType.Tool] = 2,
                [ItemType.Plant] = 2
            };

            Inventory inventory = CreateInventoryWithTypeLimiter(limits);
            inventory.TryAdd(new ItemWithType("item1", ItemType.Tool));
            inventory.TryAdd(new ItemWithType("item2", ItemType.Tool));

            inventory.TryAdd(new ItemWithType("item3", ItemType.Plant));

            IItem[] items = new IItem[2]
            {
                new ItemWithType("item4", ItemType.Plant),
                new ItemWithType("item5", ItemType.Tool)
            };

            List<IItem> canAdd = new List<IItem>();
            List<IItem> cannotAdd = new List<IItem>();
            inventory.TryAddRange(items, canAdd, cannotAdd, false);

            Assert.AreEqual(cannotAdd.Count, 1);
            Assert.AreEqual((canAdd[0] as BaseItem).Name, "item5");
        }
    }
}
