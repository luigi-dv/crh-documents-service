using System.Net;
using System.Net.Mail;

using ClearReadHub.Documents.Application.Common.Interfaces;
using ClearReadHub.Documents.Infrastructure.BackgroundServices;
using ClearReadHub.Documents.Infrastructure.Common.Persistance;
using ClearReadHub.Documents.Infrastructure.Security;
using ClearReadHub.Documents.Infrastructure.Security.IdentityProvider;
using ClearReadHub.Documents.Infrastructure.Security.PolicyEnforcer;
using ClearReadHub.Documents.Infrastructure.Security.TokenGenerator;
using ClearReadHub.Documents.Infrastructure.Services;
using ClearReadHub.Documents.Infrastructure.Storage.Persistance;
using ClearReadHub.Documents.Infrastructure.Storage.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClearReadHub.Documents.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddServices()
            .AddAuthentication(configuration)
            .AddAuthorization()
            .AddPersistence(configuration)
            .AddStorage(configuration)
            .AddBackgroundServices(configuration);

        return services;
    }

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
       services.AddEmailNotifications(configuration);

       return services;
    }

    private static IServiceCollection AddEmailNotifications(
       this IServiceCollection services,
       IConfiguration configuration)
    {
       EmailSettings emailSettings = new();
       configuration.Bind(EmailSettings.Section, emailSettings);

       if (!emailSettings.EnableEmailNotifications)
       {
           return services;
       }

       // services.AddHostedService<ReminderEmailBackgroundService>();
       services
           .AddFluentEmail(emailSettings.DefaultFromEmail)
           .AddSmtpSender(new SmtpClient(emailSettings.SmtpSettings.Server)
           {
               Port = emailSettings.SmtpSettings.Port,
               Credentials = new NetworkCredential(
                   emailSettings.SmtpSettings.Username,
                   emailSettings.SmtpSettings.Password),
           });

       return services;
   }

    /**
     * Add the services layer to the service collection
     */
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }

    /**
     * Add the persistence layer to the service collection
     */
    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext for SQL Database
        services.AddDbContext<DocumentsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ClearReadHubDatabase")));
        services.AddScoped<IStorageRepository, StorageRepository>();

        return services;
    }

    /**
     * Add the storage layer to the service collection
     */
    private static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureStorageConfiguration>(configuration.GetSection("AzureStorageSettings"));
        services.AddScoped<IAzureStorageRepository, AzureStorageRepository>();
        return services;
    }

    /**
     * Add the authorization layer to the service collection
    */
    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IIdentityProvider, IdentityProvider>();
        services.AddSingleton<IPolicyEnforcer, PolicyEnforcer>();
        return services;
    }

    /**
     * Add the authentication layer to the service collection
     */
    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services
            .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}