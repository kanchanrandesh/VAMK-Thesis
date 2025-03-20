using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace VAMK.FWMS.WebSite
{
    public class SessionRouteHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}