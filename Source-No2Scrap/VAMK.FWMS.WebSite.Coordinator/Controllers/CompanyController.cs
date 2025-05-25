using VAMK.FWMS.WebSite.Filters;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [AuthorizeAccessRule(Rule = "COMPNYVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "COMPNYADED")]
        public ActionResult AddEdit()
        {
            return View();
        }
    }
}