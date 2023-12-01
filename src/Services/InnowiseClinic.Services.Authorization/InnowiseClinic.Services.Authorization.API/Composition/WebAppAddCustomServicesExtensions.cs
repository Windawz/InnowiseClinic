using InnowiseClinic.Services.Authorization.API.Services;
using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Services.Authorization.API.Composition;

public static class WebAppAddCustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this WebApplicationBuilder builder)
    {
        return builder.Services
            .AddDbContext<AuthorizationDbContext>(options =>
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
            })
            .AddSingleton<ITokenPairFactory, JwtTokenPairFactory>()
            .AddScoped<IRegistrator, Registrator>()
            .AddScoped<IResolver, Resolver>()
            .AddTransient<IAccountRepository, AccountRepository>();
    }
}