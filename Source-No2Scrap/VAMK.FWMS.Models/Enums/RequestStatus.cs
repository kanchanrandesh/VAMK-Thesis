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
        Allocated = 2,
        IssuancePending = 3,
        Completed = 4,
        Cancelled = 5,
        Expired = 6
    }
}
