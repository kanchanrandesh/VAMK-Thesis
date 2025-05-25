using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VAMK.FWMS.WebSite.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizeAccessRuleAttribute : AuthorizeAttribute
    {
        public AuthorizeAccessRuleAttribute() : base()
        {
            this.rule = String.Empty;
        }
        private string rule;

        public string Rule
        {
            get { return this.rule; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    this.rule = value;
                }
                else
                {
                    Rule = "NOTDEFINED";
                }
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            var userIdentity = (ClaimsIdentity)httpContext.User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            List<String> roles_Str = new List<String>();
            char[] delimiters = new char[] {','};

            if(roles.Value.Split(delimiters).AsEnumerable().Contains(Rule))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary
                                   {
                                       { "action", "Index" },
                                       { "controller", "Unauthorize" }
                                   });
        }
    }
}