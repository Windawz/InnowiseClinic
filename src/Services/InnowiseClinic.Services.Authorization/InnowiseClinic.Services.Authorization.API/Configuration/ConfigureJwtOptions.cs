using Microsoft.Extensions.Options;

namespace InnowiseClinic.Services.Authorization.API.Configuration;

public class ConfigureJwtOptions : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public ConfigureJwtOptions(IConfiguration configuration, ILogger<ConfigureJwtOptions> logger)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(JwtOptions.ConfigurationKey).Bind(options);
    }
}