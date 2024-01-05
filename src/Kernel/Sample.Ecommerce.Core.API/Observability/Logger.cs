﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using System.Diagnostics;
using System.Reflection;

namespace Domain.Core.Observability;

public static class LoggerConfiguration
{
    public static ConfigureHostBuilder AddLogginSerilog(
        this ConfigureHostBuilder host, 
        string nameService, 
        IConfiguration configure)
    {
        host.UseSerilog((context, configuration) =>
        {
            string? apiData = configure["EndpointSeqLogging"];

            configuration.WriteTo
                .Console(outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext} {Properties:j} Mensagem:{Message:lj}{NewLine}{Exception}");
            
            configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Error);
            configuration.MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning);
            configuration.MinimumLevel.Override("MassTransit", LogEventLevel.Warning);
            
            configuration.Enrich.FromLogContext();            
            configuration.Enrich.WithExceptionDetails();
            configuration.Enrich.WithClientIp();            
            configuration.Enrich.WithProperty("name_service", nameService);

            configuration.Enrich.With<TraceIdEnricher>();

            if (apiData is not null) configuration.WriteTo.Seq(apiData);
        });

        return host;
    }

    public static IHostBuilder AddLogginSerilog(
        this IHostBuilder host, string nameService, 
        IConfiguration configure)
    {
        host.UseSerilog((context, configuration) =>
        {
            string? apiData = configure["EndpointSeqLogging"];

            configuration.WriteTo
                .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext} {Properties:j} Mensagem:{Message:lj}{NewLine}{Exception}");

            configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Error);
            configuration.MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning);

            configuration.Enrich.FromLogContext();
            configuration.Enrich.WithExceptionDetails();
            configuration.Enrich.WithClientIp();            
            configuration.Enrich.WithProperty("name_service", nameService);

            configuration.Enrich.With<TraceIdEnricher>();

            if (apiData is not null) configuration.WriteTo.Seq(apiData);


        });

        return host;
    }

    public static IServiceCollection ConfigureLogging(this IServiceCollection services)
    {
        services.AddLogging(e =>
        {
            e.AddSerilog(logger: Log.Logger);
            e.AddEventSourceLogger();            
        });

        return services;
    }
}

partial class TraceIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var currentActivity = Activity.Current;
        if (currentActivity == null) return;

        var traceIdProperty = propertyFactory.CreateProperty("trace_id", currentActivity.TraceId);
        var spanIdProperty = propertyFactory.CreateProperty("span_id", currentActivity.SpanId);                

        logEvent.AddPropertyIfAbsent(traceIdProperty);
        logEvent.AddPropertyIfAbsent(spanIdProperty);                
    }
}

