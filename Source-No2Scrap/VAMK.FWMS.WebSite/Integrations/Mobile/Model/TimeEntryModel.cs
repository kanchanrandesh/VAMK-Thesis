using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.Model
{
	public class TimeEntryModel
	{
		public int? EmployeeID { get; set; }
		public DateTime? Date { get; set; }
		public DateTime? TimeFrom { get; set; }
		public DateTime? TimeTo { get; set; }
		public string ActivityType { get; set; }
		public string Description { get; set; }
		public string Note { get; set; }
		public string Project { get; set; }
		public string Unit { get; set; }
		public string Opportunity { get; set; }
	}
}