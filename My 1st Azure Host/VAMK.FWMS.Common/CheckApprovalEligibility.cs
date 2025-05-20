using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JitSys.Common
{
   public  class CheckApprovalEligibility
    {
        public bool CanVerify { get; set; }
        public bool CanApprove { get; set; }
        public  int? ApprovedID { get; set; }
    }
}
