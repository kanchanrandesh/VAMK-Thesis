using VAMK.FWMS.WebSite.Filters;
using System.Web.Mvc;
using VAMK.FWMS.BizObjects;
using System.Collections.Generic;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class RequestController : Controller
    {
        // GET: Donation
        [AuthorizeAccessRule(Rule = "REQUESVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "REQUESACCE")]
        public ActionResult Accept()
        {
            return View();
        }


        [AuthorizeAccessRule(Rule = "REQUESADED")]
        public ActionResult AddEdit()
        {
            return View();
        }
    }
}