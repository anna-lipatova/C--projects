using System;


namespace UniversalInventorySystemLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PropertyLocalizationKey: Attribute
    {
        public string Key { get; private set; }

        public PropertyLocalizationKey(string key)
        {
            Key = key;
        }   
    }
}
