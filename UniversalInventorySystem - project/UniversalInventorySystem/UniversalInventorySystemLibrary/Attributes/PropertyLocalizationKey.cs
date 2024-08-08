using System;


namespace UniversalInventorySystemLibrary.Attributes
{
    /// <summary>
    /// An attribute useed to specify the localization key
    /// for a property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PropertyLocalizationKey: Attribute
    {
        public string Key { get; private set; }

        /// <summary>
        /// The localization key to be associated with the property.
        /// </summary>
        /// <param name="key"></param>
        public PropertyLocalizationKey(string key)
        {
            Key = key;
        }   
    }
}
