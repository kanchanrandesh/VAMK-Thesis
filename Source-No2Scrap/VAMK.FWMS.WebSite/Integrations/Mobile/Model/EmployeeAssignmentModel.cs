using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.Model
{
	public class EmployeeAssignmentModel
	{
		public IList<ProjectModel> ProjectList { get; set; }
		public IList<UnitModel> UnitList { get; set; }
		public IList<OpportunityModel> OpportunityList { get; set; }
		public IList<ActivityTypeModel> ActivityTypeList { get; set; }
	}
}