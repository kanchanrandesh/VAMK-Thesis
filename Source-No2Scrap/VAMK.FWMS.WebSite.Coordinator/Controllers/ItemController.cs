using VAMK.FWMS.WebSite.Filters;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        [AuthorizeAccessRule(Rule = "ITEMVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "ITEMADDED")]
        public ActionResult AddEdit()
        {
            return View();
        }
    }
}