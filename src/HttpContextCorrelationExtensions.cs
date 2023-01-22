namespace Biplov.Serilog;

public static class HttpContextCorrelationExtensions
{
    public static string CorrelationHeader(this HttpContext httpContext, string? correlationId = null)
    {
        httpContext.Request.Headers.TryGetValue(correlationId ?? "CorrelationId", out var source);
        return source.FirstOrDefault() ?? httpContext.TraceIdentifier;
    }
}