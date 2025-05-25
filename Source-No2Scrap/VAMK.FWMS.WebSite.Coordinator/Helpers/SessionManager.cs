using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Helpers
{
    public class SessionManager
    {
        public SessionSetting SessionSettings { get; set; }

        public void AuthorizeSession(int userid)
        {
            new SessionSetting().LoginSession = new Models.LoginSession()
            {
                UserID = userid,
                LoginSessionID = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        };
        }

        public void UnauthorizeSession()
        {
            new SessionSetting().LoginSession = null;
        }
    }
}