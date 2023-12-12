using InnowiseClinic.Microservices.Authorization.Application.Options;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Api.Configuration;

public class ConfigureRefreshTokenServiceOptions(IConfiguration configuration) : IConfigureOptions<RefreshTokenServiceOptions>
{
    private readonly IConfiguration _configuration = configuration;

    public void Configure(RefreshTokenServiceOptions options)
    {
        _configuration.GetRequiredSection("RefreshTokens")
            .Bind(options);
    }
}