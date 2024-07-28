using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UniversalInventorySystemLibrary.Limiters;

namespace UniversalInventorySystemLibrary.Container
{
    public class BaseItemContainer: IItemContainer
    {
        private List<IItem> items;
        private IContainerLimiter containerLimiter;
        private MonoBehaviour coroutineStarter;

        public bool TryAdd(IItem item)
        {
            return false;
        }

        public bool TryAddRange(IEnumerable<IItem> newItems)
        {
            return false;
        }
    }
}
