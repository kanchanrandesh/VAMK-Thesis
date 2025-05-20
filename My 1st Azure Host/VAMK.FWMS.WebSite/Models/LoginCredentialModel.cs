using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class LoginCredential
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}