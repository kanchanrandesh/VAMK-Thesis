using VAMK.FWMS.WebSite.Filters;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class UnitController : Controller
    {
        // GET: Unit
        [AuthorizeAccessRule(Rule = "UNITVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "UNITADED")]
        public ActionResult AddEdit()
        {
            return View();
        }
    }
}