namespace Biplov.Serilog;

internal class HttpContextInfo
{
    public string? IpAddress { get; set; }
    public string UserAgent { get; set; }
    public string Host { get; set; }
    public string Protocol { get; set; }
    public string Scheme { get; set; }
    public string User { get; set; }
    public string? Route { get; set; }

}