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
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ambev.DeveloperEvaluation.Common.Logging;

public static class LoggingExtension
{
    private static readonly DestructuringOptionsBuilder _destructuringOptionsBuilder = new DestructuringOptionsBuilder()
        .WithDefaultDestructurers()
        .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() });

    private static readonly Func<LogEvent, bool> _filterPredicate = logEvent =>
    {
        if (logEvent.Level == LogEventLevel.Information 
            && logEvent.Properties.TryGetValue("StatusCode", out var statusCode) 
                && statusCode.ToString() == "200" 
                    && logEvent.Properties.TryGetValue("Path", out var path) 
                        &&  path.ToString().Contains("/health"))
        {
            return true; 
        }

        return false; // <-- será incluído
    };

    public static WebApplicationBuilder AddDefaultLogging(this WebApplicationBuilder builder)
    {

        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.FromLogContext()
        .Filter.ByExcluding(_filterPredicate)
        .WriteTo.Console()
        .WriteTo.MongoDBBson(builder.Configuration.GetConnectionString("MongoDbLogs").ToString() ?? "", collectionName: "application_logs")
        .CreateLogger();

        return builder;
    }


    //TODO: Analisar
    //public static WebApplication UseDefaultLogging(this WebApplication app)
    //{
    //    var logger = app.Services.GetRequiredService<ILogger<Logger>>();
    //    var mode = Debugger.IsAttached ? "Debug" : "Release";
    //    logger.LogInformation("Logging enabled for '{Application}' on '{Environment}' - Mode: {Mode}",
    //        app.Environment.ApplicationName, app.Environment.EnvironmentName, mode);
    //    return app;
    //}
}
