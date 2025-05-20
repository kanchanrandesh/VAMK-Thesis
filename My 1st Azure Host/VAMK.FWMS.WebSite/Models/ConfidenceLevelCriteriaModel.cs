using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class ConfidenceLevelCriteriaModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string percentage { get; set; }
        public string weightage { get; set; }
        public string timeStamp { get; set; }

        public bool status { get; set; }
        public string message { get; set; }        
    }
}