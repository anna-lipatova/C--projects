using System;

namespace UniversalInventorySystemLibrary.Attributes
{
    /// <summary>
    /// An attribute used to mark properties 
    /// of items in the inventory system.
    /// </summary>
    public sealed class ItemProperty: Attribute
    {
        /// <summary>
        /// Gets a value indicating whether the property
        /// should be ignored.
        /// </summary>
        public bool Ignore {  get; private set; }

        /// <summary>
        /// If set to true, the property will be ignored.
        /// </summary>
        /// <param name="ignore"></param>
        public ItemProperty(bool ignore)
        {
            Ignore = ignore;
        }   
    }
}
