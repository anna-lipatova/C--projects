using System;

namespace UniversalInventorySystemLibrary.Attributes
{
    public sealed class ItemProperty: Attribute
    {
        public bool Ignore {  get; private set; }

        public ItemProperty(bool ignore)
        {
            Ignore = ignore;
        }   
    }
}
