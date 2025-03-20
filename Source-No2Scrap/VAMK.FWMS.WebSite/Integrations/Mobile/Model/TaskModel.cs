using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.Model
{
    public class TaskModel
    {
        public int? ID { get; set; }
        public string Description { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string ActivityType { get; set; }
        public bool IsDone { get; set; }
        public bool IsDelayed { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int? OpportunityID { get; set; }
        public int? ProjectID { get; set; }
        public int? UnitID { get; set; }
        public string Note { get; set; }
    }
}