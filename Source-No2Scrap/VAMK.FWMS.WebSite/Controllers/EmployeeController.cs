using VAMK.FWMS.WebSite.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [AuthorizeAccessRule(Rule = "USERVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "USERADED")]
        public ActionResult AddEdit()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "USERADED")]
        public ActionResult FamilyMember()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            ViewBag.EmployeeID = claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            ViewBag.EmployeeName = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value + " " + claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value;
            return View();
        }
    }
}