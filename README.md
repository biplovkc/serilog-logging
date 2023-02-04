# Serilog Logging
<h3 align="center">

  [![NuGet](https://img.shields.io/nuget/v/Biplov.Serilog.svg)](https://www.nuget.org/packages/Biplov.Serilog/)
  [![Downloads](https://img.shields.io/nuget/dt/Biplov.Serilog?color=brightgreen.svg)](https://www.nuget.org/packages/Biplov.Serilog/)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)

</h3>

## How to use

You should be registering serilog in your `Program.cs`.

### Inside ConfigureServices method Invoke

```
services.RegisterSerilogLogger(
            Configuration, // See [IConfiguration](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-7.0)
            typeof(Program).Assembly.GetName().Version.ToString(),
            "RequestPath like '/Account/Login%'",
            "RequestPath like '/Account/Register%'",
            "@l in ['Information'] and RequestPath like '%/_system/%'");
```

### Inside Configure method add
```
app.UseCorrelationIdMiddleware("MyCorrelationId"); // Defaults to CorrelationId if no value passed
app.UseSerilogRequestLogging(options => options.EnrichDiagnosticContext = HttpContextEnricher.HttpRequestEnricher);
```
## Example project
Example project can be found in [example folder](https://github.com/biplovkc/serilog-logging/tree/main/example)
You can run it by running following docker command inside the example folder:
```
docker-compose up --build
```
This will run following services
| Service              | Url                   |
|:---------------------|:----------------------|
| API                  | http://localhost:5000 |
| Seq                  | http://localhost:5341 |

For more info check:
https://biplov.dev/enriching-serilog-log-entries
