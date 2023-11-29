using InnowiseClinic.Services.Authorization.API.Tokens;
using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Services.Authorization.API.Infrastructure;

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
                    connectionString = builder.Configuration.GetConnectionString("Local");
                }

                if (connectionString is not null)
                {
                    options.UseSqlServer(connectionString);
                }
            })
            .AddSingleton<ITokenResponseFactory, JwtTokenResponseFactory>()
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<IRegistrator, Registrator>()
            .AddScoped<IResolver, Resolver>();
    }
}