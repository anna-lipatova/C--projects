using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UniversalInventorySystemLibrary.Attributes;

namespace UniversalInventorySystemLibrary.Items
{
    /// <summary>
    /// Provides utility methods for handing items in the inventory system.
    /// </summary>
    public static class ItemUtils
    {
        /// <summary>
        /// Represents the information of an item property.
        /// </summary>
        public class ItemPropertyInfo
        {
            /// <summary>
            /// Gets and Sets the name of the property.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets and Sets the localization key for the property name.
            /// </summary>
            public string NameLocationKey { get; set; }

            /// <summary>
            /// Gets and Sets the value of the property.
            /// </summary>
            public string Value {  get; set; }
        }

        /// <summary>
        /// Gets the properties information of the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item to get properties information from.</param>
        /// <returns>A list of ItemPropertyInfo representing the properties of the item.</returns>
        public static List<ItemPropertyInfo> GetItemPropertiesInfo<T>(T item) where T: IItem
        {
            List<ItemPropertyInfo> itemProperties = new List<ItemPropertyInfo>();
            var fields = item.GetType().GetProperties();

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
