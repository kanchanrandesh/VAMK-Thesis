using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class PasswordUpdateModel
    {
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string conformPassword { get; set; }

        public bool status { get; set; }
        public string message { get; set; }
    }
}