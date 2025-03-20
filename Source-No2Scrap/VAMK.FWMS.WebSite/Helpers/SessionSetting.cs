using VAMK.FWMS.BizObjects;
using VAMK.FWMS.Models;
using VAMK.FWMS.WebSite.Enums;
using VAMK.FWMS.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Helpers
{
    public class SessionSetting
    {
        //private LoginSession loginSession;

        public LoginSession LoginSession
        {
            get {
                return HttpContext.Current.Session[SessionKeys.LOGIN_SESSION] as LoginSession;
            }
            set {
                HttpContext.Current.Session[SessionKeys.LOGIN_SESSION] = value;
            }
        }

        public Employee EmployeeDetail
        {
            get
            {
                var empDetail = HttpContext.Current.Session[SessionKeys.EMPLOYEE] as Employee;
                if(empDetail == null && LoginSession != null && LoginSession.UserID > 0)
                {
                    empDetail= BizObjectFactory.GetEmployeeBO().GetSingle(LoginSession.UserID);
                }
                return empDetail;
            }
        }

    }
}