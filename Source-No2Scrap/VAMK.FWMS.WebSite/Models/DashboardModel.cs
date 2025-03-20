using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class DashboardModel
    {
        public string openIOUs { get; set; }
        public string openPCVs { get; set; }
        public string openMCs { get; set; }
        public string iousToApprove { get; set; }
        public string pcvsToApprove { get; set; }
        public string mcsToApprove { get; set; }       
    }
}