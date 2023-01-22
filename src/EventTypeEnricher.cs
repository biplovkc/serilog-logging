namespace Biplov.Serilog;

public class EventTypeEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var str = BitConverter
            .ToString(MurmurHash.Create32().ComputeHash(Encoding.UTF8.GetBytes(logEvent.MessageTemplate.Text)))
            .Replace("-", "");
        var property = propertyFactory.CreateProperty("EventType", str);
        logEvent.AddPropertyIfAbsent(property);
    }
}