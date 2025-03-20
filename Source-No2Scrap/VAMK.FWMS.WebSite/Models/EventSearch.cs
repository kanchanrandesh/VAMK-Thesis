using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JitSys.WebSite.Models
{
    public class EventSearch
    {
        public string EventName { get; set; }
        public string EventVenue { get; set; }
        public string EventDescription { get; set; }
        public string EventAdditionalInfo1 { get; set; }
        public string EventAdditionalInfo2 { get; set; }
        public bool AdvanceSearchedEnabled { get; set; }
    }
}