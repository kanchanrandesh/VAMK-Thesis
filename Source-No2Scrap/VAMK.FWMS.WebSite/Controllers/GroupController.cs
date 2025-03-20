using VAMK.FWMS.WebSite.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group                 
        [AuthorizeAccessRule(Rule = "USGROPVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "USGROPADED")]
        public ActionResult AddEdit()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "USGROPVIEW")]
        public ActionResult Users()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "USGROPVIEW")]
        public ActionResult Rules()
        {
            return View();
        }
    }
}