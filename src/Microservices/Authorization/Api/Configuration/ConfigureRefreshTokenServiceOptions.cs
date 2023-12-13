using InnowiseClinic.Microservices.Authorization.Application.Options;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Api.Configuration;

public class ConfigureRefreshTokenServiceOptions(IConfiguration configuration) : IConfigureOptions<RefreshTokenServiceOptions>
{
    public void Configure(RefreshTokenServiceOptions options)
    {
        configuration.GetRequiredSection("Auth:Generation:RefreshTokens")
            .Bind(options);
    }
}