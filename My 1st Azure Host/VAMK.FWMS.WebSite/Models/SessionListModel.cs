using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class SessionListModel
    {
        public string @event { get; set; }
        public string eventID { get; set; }
        public string timeFrom { get; set; }
        public string timeTo { get; set; }      
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string dateCreated { get; set; }

        public string timeFromTimePart { get; set; }
        public string timeToTimePart { get; set; }
    }
}