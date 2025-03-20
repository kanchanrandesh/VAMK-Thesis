using VAMK.FWMS.WebSite.Filters;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        [AuthorizeAccessRule(Rule = "CONTRYVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "CONTRYADED")]
        public ActionResult AddEdit()
        {
            return View();
        }
    }
}