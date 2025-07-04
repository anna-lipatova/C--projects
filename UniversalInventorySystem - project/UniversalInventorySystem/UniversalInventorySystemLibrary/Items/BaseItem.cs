using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniversalInventorySystemLibrary.Attributes;

namespace UniversalInventorySystemLibrary.Items
{
    public class BaseItem: IItem
    {
        private string _name;

        [ItemProperty(false)]
        public string Name => _name;

        public BaseItem(string name)
        {
            _name = name;
        }   
    }
}
