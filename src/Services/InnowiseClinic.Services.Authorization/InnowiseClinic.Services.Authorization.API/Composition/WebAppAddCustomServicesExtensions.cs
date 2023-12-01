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
            string? connectionString = null;
            
            if (builder.Environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
                connectionString = builder.Configuration.GetConnectionString("Local");
            }

            if (connectionString is not null)
            {
                options.UseSqlServer(connectionString);
            }
        });
    }
}