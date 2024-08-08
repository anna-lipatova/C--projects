using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UniversalInventorySystemLibrary.Items
{
    public class BaseItem: ScriptableObject, IItem
    {
        public string Name { get; set; }
    }
}
