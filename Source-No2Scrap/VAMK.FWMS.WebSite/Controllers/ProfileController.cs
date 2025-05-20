using VAMK.FWMS.WebSite.Filters;
using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View(new SessionSetting().EmployeeDetail);
        }

        public ActionResult Account()
        {
            return View(new SessionSetting().EmployeeDetail);
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult  Help()
        {
            return View();
        }
    }
}