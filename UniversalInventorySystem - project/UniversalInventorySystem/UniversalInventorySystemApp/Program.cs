using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary;
using UniversalInventorySystemLibrary.Attributes;
using UniversalInventorySystemLibrary.Container;
using UniversalInventorySystemLibrary.Items;
using UniversalInventorySystemLibrary.Limiters;
using UniversalInventorySystemLibrary.Serializer;

namespace UniversalInventorySystemApp
{
    internal class Program
    {
        //private static string json = "";
        
        public class ToolItem: BaseItem
        {
            private float _strength;

            [ItemProperty(false)]
            public float Strength => _strength;

            //for localization
            //[PropertyLocalizationKey("ToolItemName")]
            //public override string Name => base.Name;

            public ToolItem(string name, float strength) : base(name)
            {
                _strength = strength;
            }
        }

        public enum PlantType
        {
            Wheat,
            Beet
        }

        public class PlantItem: BaseItem
        {
            private PlantType _type;

            [ItemProperty(false)]
            public PlantType Type => _type;

            //for localization
            //[PropertyLocalizationKey("PlantItemName")]
            //public override string Name => base.Name;

            public PlantItem(string name, PlantType type) : base(name)
            {
                _type = type;
            }
        }

        static void Main(string[] args)
        {
            //Create simple inventory

            IItemContainer container = new BaseItemContainer();

            IContainerLimiter limiter = new CapacityLimiter(container, 5);

            Inventory inventory = new Inventory(container, limiter, new JsonSerializer());

            inventory.TryAdd(new ToolItem("Shovel", 5));

            ToolItem[] tools = new ToolItem[]
            {
                new ToolItem("Sickle", 4),
                new ToolItem("Shovel", 1)
            };

            inventory.TryAddRange(tools);
            Console.WriteLine(inventory);
            Console.WriteLine("=========================================================================");
                               
            inventory.TryAdd(new PlantItem("Wheat", PlantType.Wheat));

            Console.WriteLine();
            Console.WriteLine(inventory);
            Console.WriteLine("=========================================================================");

            inventory.SortByName();
            Console.WriteLine();
            Console.WriteLine(inventory);
            Console.WriteLine("=========================================================================");

            var filteredItems = inventory.Filter<ToolItem>("Strength", 2f, Filterer.ComparisonType.LessThan);
            Console.WriteLine();
            Console.WriteLine("FIltered: Strength < 2f");
            foreach (var item in filteredItems)
            {
                string result = item.GetType().Name;
                var properties = ItemUtils.GetItemPropertiesInfo(item);
                foreach(var property in properties)
                {
                    result += $", {property.Name} = {property.Value}";
                }

                Console.WriteLine(result);
            }
            Console.WriteLine("=========================================================================");

            string toJson = inventory.Serialize();
            Console.WriteLine();
            Console.WriteLine("Serialized Inventory to JSON:");
            Console.WriteLine(toJson);

        }
    }
}