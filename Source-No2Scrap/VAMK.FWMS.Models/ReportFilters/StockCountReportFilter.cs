using System;
using System.Collections.Generic;

namespace VAMK.FWMS.Models.ReportFilters
{
    public class StockCountReportFilter
    {
        public string GroupBy { get; set; }
        public List<int> ItemIDs { get; set; }
        public int? LoggedEmployeeID { get; set; }
    }
}
