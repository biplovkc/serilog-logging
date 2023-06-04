using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using System.Security.Principal;

using UAParser;

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
        var userAgent = httpContext.Request.Headers?.FirstOrDefault(s => "user-agent".Equals(s.Key, StringComparison.OrdinalIgnoreCase)).Value;
        httpContextInfo.UserAgent = userAgent is not null ? userAgent.ToString() : "";
        // get a parser with the embedded regex patterns
        var uaParser = Parser.GetDefault();

        // get a parser using externally supplied yaml definitions
        // var uaParser = Parser.FromYaml(yamlString);

        var clientInfo = uaParser.Parse(userAgent);

        diagnosticContext.Set("Route", httpContextInfo.Route);
        diagnosticContext.Set("User", httpContextInfo.User);
        diagnosticContext.Set("Host", httpContextInfo.Host);
        diagnosticContext.Set("IpAddress", httpContextInfo.IpAddress);
        diagnosticContext.Set("Protocol", httpContextInfo.Protocol);
        diagnosticContext.Set("Scheme", httpContextInfo.Scheme);
        if (!string.IsNullOrWhiteSpace(clientInfo?.Device?.Family))
            diagnosticContext.Set("Device", clientInfo.Device.Family);
        
        if (!string.IsNullOrWhiteSpace(clientInfo?.OS?.Family))
            diagnosticContext.Set("OperatingSystem", clientInfo.OS.Family);

        if (!string.IsNullOrWhiteSpace(clientInfo?.UA?.Family))
            diagnosticContext.Set("Browser", clientInfo.UA.Family);
    }

    private static string GetUserInfo(IPrincipal user) => user.Identity is { IsAuthenticated: true } ? user.Identity.Name : Environment.UserName;
}
