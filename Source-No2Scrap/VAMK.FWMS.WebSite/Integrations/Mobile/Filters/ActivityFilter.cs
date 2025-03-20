using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.Filters
{
    public class ActivityFilter
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int? LoginUser { get; set; }
        public int? ActivityID { get; set; }
        public DateTime ActivityDate { get; set; }
        public DateTime Date { get; set; }
              
    }
}