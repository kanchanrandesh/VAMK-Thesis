using System;
using System.Web.Http.Filters;

namespace VAMK.FWMS.WebSite
{
    internal class ClaimsAuthorizeAttribute : IFilter
    {
        public bool AllowMultiple
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}