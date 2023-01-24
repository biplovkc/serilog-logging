using Serilog;

namespace Biplov.Serilog.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterSerilogLogger(
                Configuration,
                "0.1",
                "RequestPath like '/api/ignore%'",
                "@l in ['Information'] and RequestPath like '%/_system/%'");
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCorrelationIdMiddleware("TestCorrelationId");
            app.UseSerilogRequestLogging(options => options.EnrichDiagnosticContext = HttpContextEnricher.HttpRequestEnricher);
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Use((context, next) =>
            {
                context.Response.Headers.Add("TestCorrelationId", context.CorrelationHeader());
                return next.Invoke();
            });
        }
    }
}
