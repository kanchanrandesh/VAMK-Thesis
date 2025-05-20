using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class EmployeeRecipientModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }

        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string recipientId { get; set; }
        public string recipientName { get; set; }

        public string displayName { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

       
    }
}