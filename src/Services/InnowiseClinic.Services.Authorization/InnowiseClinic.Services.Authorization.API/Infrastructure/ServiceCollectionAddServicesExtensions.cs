using InnowiseClinic.Services.Authorization.API.Tokens;
using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services;

namespace InnowiseClinic.Services.Authorization.API.Infrastructure;

public static class ServiceCollectionAddServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        return services
            .AddDbContext<AuthorizationDbContext>()
            .AddSingleton<ITokenResponseFactory, JwtTokenResponseFactory>()
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<IRegistrator, Registrator>()
            .AddScoped<IResolver, Resolver>();
    }
}