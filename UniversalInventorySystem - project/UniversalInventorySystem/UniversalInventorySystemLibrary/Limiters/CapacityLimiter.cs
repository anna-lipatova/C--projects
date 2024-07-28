using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Limiters
{
    public class CapacityLimiter: IContainerLimiter
    {
        public bool CanAddItem(IItem item)
        {
            throw new NotImplementedException();
        }
    }
}
