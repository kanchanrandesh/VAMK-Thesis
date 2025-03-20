#region © 2016 Copyright TechSys (Pvt) Ltd
//
// All rights are reserved. Reproduction or transmission in
// whole or in part, in any form or by any means, electronic,
// mechanical or otherwise, is prohibited without the prior
// written consent of the copyright owner.
//
// Filename     : AuthorizeUser.cs
// Created By   : Deepakumar
// Date         : 2017-Jan-11, Wed
// Description  : Filter for Authorization
//
// Modified By  : 
// Date         : 
// Purpose      : 
//
#endregion

using JitSys.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JitSys.WebSite.Filters
{
    public class AuthorizeUser : System.Web.Mvc.AuthorizeAttribute
    {

        /// <summary>
        /// Overiding base implementation for the OnAuthorization
        /// Check for the AllowAnonymousUsers for exception actions
        /// </summary>
        /// <param name="filterContext">Filtered Context</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Check for the full controller for the AllowAnonymousUsers
            // Uncomment if required for bypass the full controller.
            //if (!(filterContext.Controller is QnP.Controllers.LoginController))
            //    ValidateUserCredentials(filterContext);

            if (filterContext.Controller is JitSys.WebSite.Controllers.HomeController ||
                filterContext.Controller is JitSys.WebSite.Controllers.HomeController)
            {
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Expires = -1500;
                HttpContext.Current.Response.CacheControl = "no-cache";
                HttpContext.Current.Response.Cache.SetNoStore();
            }

            if (!(filterContext.Controller is JitSys.WebSite.Controllers.HomeController && filterContext.ActionDescriptor.ActionName.Equals("Index")))
            {
                if (!ValidateAuthentication.IsUserLoggedIn())
                {
                    filterContext.Result = ValidateAuthentication.GetRouteResult("home", "Index");
                    return;
                }
            }

            // Check for the AllowAnonymousUsers is set for the perticular Action.
            // If set to AllowAnonymousUsers, then skip the autharization implementation.
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymous), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymous), true);
            if (!skipAuthorization)
            {
                if (ValidateAuthentication.IsUserLoggedIn())
                {
                    return;
                }
                else
                {
                    var url = new UrlHelper(filterContext.RequestContext);
                    var logonUrl = url.Action("Index", "Login", new { reason = "youAreAuthorisedButNotAllowedToViewThisPage" });
                    filterContext.Result = new RedirectResult(logonUrl);

                }
            }
        }

        private void ValidateUserCredentials(AuthorizationContext filterContext)
        {
            ValidateAuthentication _authorizationRule = new ValidateAuthentication();
            ValidateAuthentication _authenticationRule = new ValidateAuthentication() { Successor = _authorizationRule };
            _authenticationRule.HandleRequest(filterContext);
        }
    }

    public class ValidateAuthentication : ISecurityServiceHandler
    {
        public ISecurityServiceHandler Successor { get; set; }

        internal static ActionResult GetRouteResult(string controller, string action)
        {
            return new RedirectToRouteResult(
                                 new RouteValueDictionary {
                  { "controller", controller },
                  { "action", action }
                });
        }

        internal static bool IsUserLoggedIn()
        {
            var isUserLoggedIn = new SessionSetting().LoginSession;

            return (isUserLoggedIn != null);

        }

        public void HandleRequest(AuthorizationContext filterContext)
        {
            var isAuthorized = false;
            if (filterContext.ActionDescriptor.IsDefined(typeof(UserAccessRights), true))
            {
                var attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(UserAccessRights), true);
                if (attributes.Length > 0)
                {
                    var userRights = (UserAccessRights)attributes[0];
                    if (userRights != null)
                    {
                        isAuthorized = !userRights.IsAuthorizationrequired || CheckAuthorization(userRights);
                    }
                }
            }
            if (isAuthorized)
            {
                this.TrySuccessor(filterContext);
            }
            else
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Unauthorized"
                };
            }
        }

        private bool CheckAuthorization(UserAccessRights userRights)
        {
            return true;
        }
    }

    public interface ISecurityServiceHandler
    {
        /// <summary>
        /// Sucssessor on sucsussfull validation
        /// </summary>
        ISecurityServiceHandler Successor { get; set; }
        void HandleRequest(AuthorizationContext filterContext);
    }

    public static class SecurityRuleServiceHandlerExtension
    {
        public static void TrySuccessor(this ISecurityServiceHandler current, AuthorizationContext filterContext)
        {
            ///Check for the current sucsussor is available
            if (current.Successor != null)
                current.Successor.HandleRequest(filterContext);
        }
    }
}

    