using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class HomeComponentsController : Controller
    {
        // GET: HomeComponents
        public ActionResult SideBar()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            List<String> userRoles = new List<String>();
            return View(userRoles);
        }
    }
}