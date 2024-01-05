using InnowiseClinic.Microservices.Authorization.Application.Options;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Api.Configuration;

public class ConfigureRefreshTokenServiceOptions : IConfigureOptions<RefreshTokenServiceOptions>
{
    private readonly IConfiguration _configuration;

    public ConfigureRefreshTokenServiceOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(RefreshTokenServiceOptions options)
    {
        _configuration.GetRequiredSection("Auth:Generation:RefreshTokens")
            .Bind(options);
    }
}