using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models.Enums
{
    public enum RequestStatus
    {
        AllocationPending = 1,
        Allocated,
        Dispatched = 2,
        Cancelled = 3,
        Expired = 4
    }
}
