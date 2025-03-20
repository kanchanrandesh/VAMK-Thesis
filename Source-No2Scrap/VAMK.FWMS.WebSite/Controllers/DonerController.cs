using VAMK.FWMS.WebSite.Filters;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class DonerController : Controller
    {
        // GET: Doner
        [AuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "DEPRTMADED")]
        public ActionResult AddEdit()
        {
            return View();
        }
    }
}