using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Streameus.Hooks
{
    /// <summary>
    /// Delegating Handler to parse the PUT and DELETE request from JS ajax requests 
    /// </summary>
    public class XHttpMethodOverrideDelegatingHandler : DelegatingHandler
    {
        private static readonly string[] HttpMethods = {"PUT", "DELETE"};
        private const string HttpMethodOverrideHeader = "X-HTTP-Method-Override";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Method != HttpMethod.Post || !request.Headers.Contains(HttpMethodOverrideHeader))
                return base.SendAsync(request, cancellationToken);
            var httpMethod = request.Headers.GetValues(HttpMethodOverrideHeader).FirstOrDefault();
            if (httpMethod != null && HttpMethods.Contains(httpMethod, StringComparer.InvariantCultureIgnoreCase))
                request.Method = new HttpMethod(httpMethod);
            return base.SendAsync(request, cancellationToken);
        }
    }
}