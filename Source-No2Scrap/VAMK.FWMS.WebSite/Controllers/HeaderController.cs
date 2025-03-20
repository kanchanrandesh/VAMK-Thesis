using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class HeaderController : Controller
    {
        // GET: Header
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var role = identity.FindFirst(ClaimTypes.Role).Value;

            ViewBag.EmployeeFirstName = identity.FindFirst(ClaimTypes.GivenName).Value;
            ViewBag.EmployeeLastName = identity.FindFirst(ClaimTypes.Surname).Value;

            return View();
        }
    }
}