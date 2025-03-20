using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}