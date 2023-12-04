using FluentValidation;
using InnowiseClinic.Services.Authorization.API.Auth.Configuration;
using InnowiseClinic.Services.Authorization.API.Auth.DataTransfer;
using InnowiseClinic.Services.Authorization.API.Auth.Services;
using InnowiseClinic.Services.Authorization.API.Auth.Validation;
using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Services.Authorization.API.Composition;

public static class CustomComponentsWebAppExtensions
{
    public static WebApplicationBuilder AddCustomComponents(this WebApplicationBuilder builder)
    {
        AddDataLayerServices(builder);
        AddServiceLayerServices(builder);
        AddAPILayerServices(builder);
        ConfigureOptions(builder);

        return builder;
    }

    private static void ConfigureOptions(WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<ConfigureMvcOptions>();
        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();
        builder.Services.ConfigureOptions<ConfigureJwtOptions>();
    }

    private static void AddAPILayerServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenPairFactory, JwtTokenPairFactory>()
            .AddScoped<IValidator<RegisterSelfInput>, RegisterSelfInputValidator>()
            .AddScoped<IValidator<RegisterOtherInput>, RegisterOtherInputValidator>();
    }

    private static void AddServiceLayerServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRegistrator, Registrator>()
            .AddScoped<IResolver, Resolver>()
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<ILogInAccessor, LogInAccessor>();
    }

    private static void AddDataLayerServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AuthorizationDbContext>(options =>
        {
            string? connectionString = DetermineConnectionString(builder.Configuration, builder.Environment);
            
            if (builder.Environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }

            if (connectionString is not null)
            {
                options.UseSqlServer(connectionString);
            }
        });
    }

    private static string? DetermineConnectionString(IConfiguration configuration, IWebHostEnvironment environment)
    {
        string? connectionString = configuration.GetConnectionString("Default");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = null;
            if (environment.IsDevelopment())
            {
                connectionString = configuration.GetConnectionString("Local");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    connectionString = null;
                }
            }
        }
        return connectionString;
    }
}