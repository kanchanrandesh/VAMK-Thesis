using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace VAMK.FWMS.WebSite.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HttpAuthorizeAccessRuleAttribute : AuthorizeAttribute
    {
        public HttpAuthorizeAccessRuleAttribute() : base() {
            this.rule = String.Empty;
        }

        private string rule;

        public string Rule
        {
            get { return this.rule; }
            set {
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


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)HttpContext.Current.Session["ClaimsPrincipal"];
            actionContext.RequestContext.Principal = principal;
            var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            List<string> roles_Str = new List<string>();
            List<string> controller_rules = new List<string>();

            char[] delimiters = new char[] { ',' };

            if (!Rule.Contains(','))
            {
                controller_rules.Add(Rule);
            }
            else
            {
                controller_rules = Rule.Split(',').ToList();
            }

            foreach (var item in controller_rules)
            {
                if (controller_rules != null && roles.Value.Split(delimiters).AsEnumerable().Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var httpMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            httpMessage.Headers.Add("ErrorCode", rule);
            actionContext.Response = httpMessage;
        }
    }
}