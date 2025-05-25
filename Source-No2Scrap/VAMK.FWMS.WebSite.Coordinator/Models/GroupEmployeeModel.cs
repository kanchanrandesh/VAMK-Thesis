using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class GroupEmployeeModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string groupId { get; set; }
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public bool selected { get; set; }
    }
}