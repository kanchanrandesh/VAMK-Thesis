using VAMK.FWMS.WebSite.Filters;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class ContactPersonController : Controller
    {
        // GET: Contact
        [AuthorizeAccessRule(Rule = "CONTCTVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "CONTCTADED")]
        public ActionResult AddEdit()
        {
            return View();
        }
    }
}