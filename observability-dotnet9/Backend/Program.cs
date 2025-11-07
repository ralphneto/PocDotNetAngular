using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration (Loki sink)
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.GrafanaLoki("http://loki:3100")
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// OpenTelemetry setup
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation()
               .AddHttpClientInstrumentation()
               .AddRuntimeInstrumentation()
               .AddPrometheusExporter();
    })
    .WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation()
               .AddHttpClientInstrumentation()
               .AddSqlClientInstrumentation()
               .AddOtlpExporter(opt =>
               {
                   opt.Endpoint = new Uri("http://otel-collector:4317");
               });
    });

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new { status = "ok", ts = DateTime.UtcNow }));
app.MapControllers();

// Expose Prometheus metrics scraping endpoint (default /metrics)
app.MapPrometheusScrapingEndpoint();

app.Run();
