using System.Reflection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Byndyusoft.AspNetCore.Cors
{
    public class InsecuredCorsService : CorsService, ICorsService
    {
        public InsecuredCorsService(IOptions<CorsOptions> options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
        {
        }

        CorsResult ICorsService.EvaluatePolicy(HttpContext context, CorsPolicy policy)
        {
            if (policy.AllowAnyOrigin && policy.SupportsCredentials)
                return EvaluatePolicyInsecure(context, policy);

            return EvaluatePolicy(context, policy);
        }

        private CorsResult EvaluatePolicyInsecure(HttpContext context, CorsPolicy policy)
        {
            var requestHeaders = context.Request.Headers;
            var origin = requestHeaders[CorsConstants.Origin];

            var isOptionsRequest = HttpMethods.IsOptions(context.Request.Method);
            var isPreflightRequest = isOptionsRequest && requestHeaders.ContainsKey(CorsConstants.AccessControlRequestMethod);

            var corsResult = new CorsResult
            {
                IsPreflightRequest = isPreflightRequest,
                IsOriginAllowed = IsOriginAllowed(policy, origin)
            };

            if (isPreflightRequest)
            {
                EvaluatePreflightRequest(context, policy, corsResult);
            }
            else
            {
                EvaluateRequest(context, policy, corsResult);
            }

            return corsResult;
        }

        private bool IsOriginAllowed(CorsPolicy policy, StringValues origin)
        {
            var method = typeof(InsecuredCorsService).BaseType!.GetMethod(nameof(IsOriginAllowed),
                BindingFlags.Instance | BindingFlags.NonPublic);
            return (bool)method.Invoke(this, new object[]{policy, origin});
        }
    }
}