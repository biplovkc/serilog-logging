using Biplov.Serilog.WebAPI;
using Serilog;

try
{
    var host = CreateHostBuilder(args);

    await host.Build().RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Api could not start");
    return 1;
}
finally
{
    Log.CloseAndFlush();
    await Task.Delay(TimeSpan.FromMilliseconds(200));
}
static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureHostConfiguration(configurationBuilder =>
        {
            configurationBuilder
                .AddEnvironmentVariables("DOTNET_")
                .AddCommandLine(args);
        })
        .ConfigureAppConfiguration((hostingContext, config) =>
            config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile	("appsettings.Development.json", optional:true, reloadOnChange: true))
        .ConfigureLogging((_, logging) => logging.ClearProviders())
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });


