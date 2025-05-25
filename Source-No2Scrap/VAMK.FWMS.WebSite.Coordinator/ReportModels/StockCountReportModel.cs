using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.ReportModels
{
    public class StockCountReportModel
    {
        public string groupBy { get; set; }
        public string itemIds { get; set; }
        public string loggedEmployeeId { get; set; }

        public static implicit operator VAMK.FWMS.Models.ReportFilters.StockCountReportFilter(StockCountReportModel e)
        {
            var list = e.itemIds.Split(',');
            var idList = new List<int>();
            foreach (var item in list)
            {
                int? id = Utility.ParseInt(item);
                if (id != null)
                    idList.Add(id.Value);
            }

            return new VAMK.FWMS.Models.ReportFilters.StockCountReportFilter()
            {
                GroupBy = e.groupBy,               
                LoggedEmployeeID = Utility.ParseInt(e.loggedEmployeeId),
                ItemIDs = idList
            };
        }
    }
}