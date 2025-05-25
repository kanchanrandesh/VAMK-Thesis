using VAMK.FWMS.BizObjects;
using VAMK.FWMS.Models;
//using VAMK.FWMS.WebSite.Filters;
using VAMK.FWMS.WebSite.Helpers;
using VAMK.FWMS.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            var claims = userIdentity.Claims;
            Session.Add("ClaimsPrincipal", principal);
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            ViewData.Add("Rules", roles.Value.Split(new char[] { ',' }));

            int userId;
            if (int.TryParse(claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value, out userId))
            {
                new SessionSetting().LoginSession = new Models.LoginSession()
                {
                    UserID = userId,
                    LoginSessionID = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                };
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginCredential loginUser)
        {
            SystemUser user;
            user = BizObjectFactory.GetEmployeeBO().ValidateLoginCredential(loginUser.Username, loginUser.Password);
            if (user != null && user.ID.Value > 0)
            {
                new SessionManager().AuthorizeSession(user.ID.Value);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.msg = "Invalid username or password";
                return View();

            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Header()
        {
            return View();
        }
    }
}
