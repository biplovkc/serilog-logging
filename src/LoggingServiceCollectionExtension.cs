﻿namespace Biplov.Serilog;

public static class LoggingServiceCollectionExtension
{
    public static IServiceCollection RegisterSerilogLogger(this IServiceCollection services, IConfiguration configuration, string version, params string[] pathsToExclude)
    {
        var loggerConfig = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration);

        var logger = loggerConfig
            .Enrich.FromLogContext()
            .Enrich.WithAssemblyName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithSpan();

        if (!string.IsNullOrWhiteSpace(version))
        {
            logger.Enrich.WithProperty("Version", version);
        }

        if (pathsToExclude.Any())
        {
            foreach (var path in pathsToExclude)
            {
                logger.Filter.ByExcluding(path);
            }
        }

        Log.Logger = logger.CreateLogger();
        services.AddSingleton(Log.Logger);

        return services;
    }

    public static IServiceCollection RegisterSerilogLogger(this IServiceCollection services, IConfiguration configuration, string version, Dictionary<string, LogEventLevel> overrides, params string[] pathsToExclude)
    {
        var loggerConfig = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration);

        if (overrides is not null)
            foreach (var overRide in overrides)
                loggerConfig.MinimumLevel.Override(overRide.Key, overRide.Value);
        
        var logger = loggerConfig
            .Enrich.FromLogContext()
            .Enrich.WithAssemblyName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithSpan();
            
        if (!string.IsNullOrWhiteSpace(version))
        {
            logger.Enrich.WithProperty("Version", version);
        }

        if (pathsToExclude.Any())
        {
            foreach (var path in pathsToExclude)
            {
                logger.Filter.ByExcluding(path);
            }
        }

        Log.Logger = logger.CreateLogger();
        services.AddSingleton(Log.Logger);
        
        return services;
    }
}