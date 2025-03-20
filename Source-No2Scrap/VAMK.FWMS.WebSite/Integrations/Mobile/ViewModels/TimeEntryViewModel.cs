using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAMK.FWMS.WebSite.Integrations.Mobile;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.ViewModels
{
    public class TimeEntryViewModel
    {
        public int? ID { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string ActivityType { get; set; }
        public int? EmployeeID { get; set; }
        public int? ProjectID { get; set; }
        public int? UnitID { get; set; }
        public int? OpportunityID { get; set; }
        public int? TaskID { get; set; }
        public bool IsDone { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}