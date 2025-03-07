using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using ToDoList.Business.Contracts;
using ToDoList.Business.Services;
using ToDoList.Common.DTOs;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Context;

namespace ToDoList.Web;

internal static class DependencyInjection
{
    internal static IServiceCollection InjectContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContextFactory<ToDoListContext>(optionsAction =>
        {
            optionsAction.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!);
        });

    internal static IServiceCollection InjectAddEndpointsApiExplorer(this IServiceCollection services) =>
       services.AddEndpointsApiExplorer();

    internal static IServiceCollection InjectControllers(this IServiceCollection services) =>
        services.AddControllers()
                .Services;

    internal static IServiceCollection InjectAddSwaggerGen(this IServiceCollection services) =>
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Beacon", Version = "v1" });

            c.CustomSchemaIds(type => type.FullName);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
            });
        });

    internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>();

    internal static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        return services;
    }

    internal static IServiceCollection InjectFluentValidation(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<AddTaskDto>()
        .AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

    internal static IServiceCollection InjectLogger(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(
                path: "logs/log.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                fileSizeLimitBytes: 10 * 1024 * 1024,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
            )
            .MinimumLevel.Error()
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        return services;
    }

    internal static IServiceCollection InjectOutputCache(this IServiceCollection services) =>
        services.AddOutputCache();

}
