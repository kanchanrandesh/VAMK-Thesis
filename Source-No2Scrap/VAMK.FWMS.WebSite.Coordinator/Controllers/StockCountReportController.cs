using VAMK.FWMS.WebSite.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class StockCountReportController : Controller
    {
        // GET: StockCountReport 
        [AuthorizeAccessRule(Rule = "STKCNTREPO")]
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            ViewBag.EmployeeID = claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            return View();
        }
    }
}