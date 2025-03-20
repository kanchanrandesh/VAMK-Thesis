using VAMK.FWMS.BizObjects;
using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.WebSite.Helpers;
using VAMK.FWMS.WebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace VAMK.FWMS.WebSite.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        // POST:// Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginCredential model, string ReturnUrl)
        {
            Employee user, employeeByUsername;
            string rolesStr = "";

            employeeByUsername = BizObjectFactory.GetEmployeeBO().GetEmployeeByUserName(model.Username);

            if (employeeByUsername == null)
            {
                ViewBag.msg = "Invalid username";
                return View();
            }

            if (employeeByUsername.IsLocked)
            {
                ViewBag.msg = "Your account is locked. Please contact your System Administrator to unlock";
                return View();
            }

            user = BizObjectFactory.GetEmployeeBO().ValidateLoginCredential(model.Username, model.Password);

            //if (user != null)
            //{
            //    var pwUnchangedPeriod = DateTime.Today - user.PasswordResetDate;
            //    if (pwUnchangedPeriod.Value.Days >= 5)
            //    {
            //        return ChangePassword();
            //        //return RedirectToAction("Index", "ChangePassword");
            //    }
            //}

            if (user != null && user.ID > 0 && user.IsLocked != true)
            {
                new SessionManager().AuthorizeSession(user.ID.Value);
                FormsAuthentication.SetAuthCookie(user.ID.Value.ToString(), false);

                var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Sid,user.ID.Value.ToString()),
                            new Claim(ClaimTypes.Name,String.Format("{0}-{1} {2} " , user.ID.Value, user.FirstName, user.LastName)),
                            new Claim(ClaimTypes.GivenName, user.FirstName),
                            new Claim(ClaimTypes.Surname, user.LastName),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("CompanyID",user.CompanyID.ToString())
                        },
                        DefaultAuthenticationTypes.ApplicationCookie,
                        ClaimTypes.Name, ClaimTypes.Role);

                var roles = BizObjectFactory.GroupEmployeeBO().GetRulesForUser(user.ID.Value);
                if (roles != null && roles.Count > 0)
                {
                    rolesStr = String.Join(",", roles.ToArray());
                }
                identity.AddClaim(new Claim(ClaimTypes.Role, rolesStr));

                Authentication.SignIn(new AuthenticationProperties
                {
                    IsPersistent = model.RememberPassword
                }, identity);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                Thread.CurrentPrincipal = principal;
                HttpContext.User = principal;
                HttpContext.Session.Add("ClaimsPrincipal", principal);

                if (employeeByUsername.UnSuccessfulLoginAttempts != null && employeeByUsername.UnSuccessfulLoginAttempts.Value > 0)
                {
                    employeeByUsername.UnSuccessfulLoginAttempts = 0;
                    employeeByUsername.State = VAMK.FWMS.Models.Interfaces.State.Modified;
                    var transObject = new EmployeeFacade(WebSettingProvider.GetWebSettings()).SaveForAttempt(employeeByUsername);
                }

                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                ViewBag.msg = "Invalid username or password. Account will be locked after 3 unsuccessful attempts";
                Employee obj = user;

                if (employeeByUsername != null)
                {
                    if (employeeByUsername.UnSuccessfulLoginAttempts == null || employeeByUsername.UnSuccessfulLoginAttempts == 0)
                        employeeByUsername.UnSuccessfulLoginAttempts = 1;
                    else if (employeeByUsername.UnSuccessfulLoginAttempts == 1)
                        employeeByUsername.UnSuccessfulLoginAttempts = 2;
                    else if (employeeByUsername.UnSuccessfulLoginAttempts == 2)
                    {
                        employeeByUsername.UnSuccessfulLoginAttempts = 3;
                        employeeByUsername.IsLocked = true;

                        employeeByUsername.State = VAMK.FWMS.Models.Interfaces.State.Modified;
                        var transObjectLocked = new EmployeeFacade(WebSettingProvider.GetWebSettings()).SaveForAttempt(employeeByUsername);

                        ViewBag.msg = "Your account is locked. Please contact your System Administrator to unlock";
                        return View();
                    }

                    employeeByUsername.State = VAMK.FWMS.Models.Interfaces.State.Modified;

                    var transObject = new EmployeeFacade(WebSettingProvider.GetWebSettings()).SaveForAttempt(employeeByUsername);
                }
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return RedirectToAction("Index", "ChangePassword");
        }
    }
}