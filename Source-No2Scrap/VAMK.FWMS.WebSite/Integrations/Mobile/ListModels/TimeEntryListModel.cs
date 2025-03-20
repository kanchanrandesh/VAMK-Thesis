using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.ListModels
{
    public class TimeEntryListModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
		public List<Model.TimeEntryModel> TimeEntryList { get; set; }
    }
}