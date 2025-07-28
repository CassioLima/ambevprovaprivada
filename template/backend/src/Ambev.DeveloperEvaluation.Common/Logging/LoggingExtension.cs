using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.Extensions.Configuration;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Exceptions;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Common.Logging;

public static class LoggingExtension
{
    private static readonly DestructuringOptionsBuilder _destructuringOptionsBuilder = new DestructuringOptionsBuilder()
        .WithDefaultDestructurers()
        .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() });

    private static readonly Func<LogEvent, bool> _filterPredicate = logEvent =>
    {
        if (logEvent.Level != LogEventLevel.Information)
            return true;

        logEvent.Properties.TryGetValue("StatusCode", out var statusCode);
        logEvent.Properties.TryGetValue("Path", out var path);

        if ((statusCode?.ToString() == "200") && (path?.ToString().Contains("/health") ?? false))
            return false;

        return true;
    };

    public static WebApplicationBuilder AddDefaultLogging(this WebApplicationBuilder builder)
    {

        builder.Host.UseSerilog((context, config) =>
        {
            var mongoUrl = new MongoUrl(context.Configuration.GetConnectionString("MongoDbLogs"));

            config
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails(_destructuringOptionsBuilder)
                .WriteTo.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}",
                    theme: SystemConsoleTheme.Colored)
                .WriteTo.File(
                    "logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}")
                .WriteTo.MongoDBBson(mongoUrl.ToString(), collectionName: "application_logs");
        });

        builder.Services.AddLogging();
        return builder;
    }

    //public static WebApplication UseDefaultLogging(this WebApplication app)
    //{
    //    var logger = app.Services.GetRequiredService<ILogger<Logger>>();
    //    var mode = Debugger.IsAttached ? "Debug" : "Release";
    //    logger.LogInformation("Logging enabled for '{Application}' on '{Environment}' - Mode: {Mode}",
    //        app.Environment.ApplicationName, app.Environment.EnvironmentName, mode);
    //    return app;
    //}
}
