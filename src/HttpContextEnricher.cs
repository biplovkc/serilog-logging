using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using System.Security.Principal;

namespace Biplov.Serilog;

public static class HttpContextEnricher
{
    public static void HttpRequestEnricher(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var httpContextInfo = new HttpContextInfo
        {
            User = GetUserInfo(httpContext.User),
            Host = httpContext.Request.Host.ToString(),
            IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
            Protocol = httpContext.Request.Protocol,
            Scheme = httpContext.Request.Scheme,
            Route = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata
                .GetMetadata<RouteNameMetadata>()?.RouteName
        };

        diagnosticContext.Set("HttpContext", httpContextInfo, true);
    }

    private static string GetUserInfo(IPrincipal user) => user.Identity is { IsAuthenticated: true } ? user.Identity.Name : Environment.UserName;
}