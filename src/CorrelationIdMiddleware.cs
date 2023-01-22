namespace Biplov.Serilog;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _correlationId;

    public CorrelationIdMiddleware(RequestDelegate next, string? correlationId = null)
    {
        _next = next ?? throw new ArgumentException(nameof(next));
        _correlationId = correlationId ?? "CorrelationId";
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext is null)
            throw new ArgumentException(nameof(httpContext));

        using (LogContext.PushProperty(_correlationId, httpContext.CorrelationHeader()))
            await _next(httpContext);
    }
}

public static class CorrelationIdMiddlewareExtensions
{
    /// <summary>
    /// Registers CorrelationId Middleware via
    /// <see cref="https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.iapplicationbuilder?view=aspnetcore-7.0">IApplicationBuilder</see>
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="correlationId">Custom value for CorrelationId name can be passes here. Default is null</param>
    /// <returns>IApplicationBuilder</returns>
    public static IApplicationBuilder UseCorrelationIdMiddleware(
        this IApplicationBuilder builder, string? correlationId = null)
    {
        return builder.UseMiddleware<CorrelationIdMiddleware>(correlationId);
    }
}