using VAMK.FWMS.WebSite.Filters;
using System.Web.Mvc;
using VAMK.FWMS.BizObjects;
using System.Collections.Generic;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class DonationController : Controller
    {
        // GET: Donation
        [AuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "DEPRTMRECE")]
        public ActionResult Receive()
        {
            return View();
        }

        [AuthorizeAccessRule(Rule = "DEPRTMADED")]
        public ActionResult AddEdit()
        {
            var ItemList = new List<Models.SelectObjectModel>();
            ItemList.Add(new Models.SelectObjectModel { id = "-", name = "-- Select --" });
            foreach (var item in BizObjectFactory.GetItemBO().GetAll())
                ItemList.Add(new Models.SelectObjectModel { id = item.ID.Value.ToString(), name = item.Code + "-" + item.Name });

            ViewBag.Items = ItemList;

            return View();
        }
    }
}