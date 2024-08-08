using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UniversalInventorySystemLibrary.Attributes;

namespace UniversalInventorySystemLibrary.Items
{
    public static class ItemUtils
    {
        public class ItemPropertyInfo
        {
            public string Name { get; set; }
            public string NameLocationKey { get; set; }
            public string Value {  get; set; }
        }

        public static List<ItemPropertyInfo> GetItemPropertiesInfo<T>(T item) where T: IItem
        {
            List<ItemPropertyInfo> itemProperties = new List<ItemPropertyInfo>();
            var fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute<ItemProperty>();
                if (attribute != null && attribute.Ignore == false)
                {
                    ItemPropertyInfo info = new ItemPropertyInfo();
                    info.Name = field.Name;
                    info.Value = field.GetValue(item).ToString();
                    var localizationAttribute = field.GetCustomAttribute<PropertyLocalizationKey>();

                    if(localizationAttribute != null)
                    {
                        info.NameLocationKey = localizationAttribute.Key;
                    }
                    itemProperties.Add(info);
                }
            }

            return itemProperties;
        }
    }
}
