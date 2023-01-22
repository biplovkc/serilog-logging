namespace Biplov.Serilog;

public static class EventTypeLoggerConfigurationExtensions
{
    public static LoggerConfiguration WithEventType(this LoggerEnrichmentConfiguration enrichmentConfiguration) =>
        enrichmentConfiguration.With<EventTypeEnricher>();
}