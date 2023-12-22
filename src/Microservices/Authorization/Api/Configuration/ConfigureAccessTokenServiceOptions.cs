using InnowiseClinic.Microservices.Authorization.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Api.Configuration;

public class ConfigureAccessTokenServiceOptions : IConfigureOptions<AccessTokenServiceOptions>
{
    private readonly IConfiguration _configuration;
    private readonly JwtBearerOptions _jwtBearerOptions;

    public ConfigureAccessTokenServiceOptions(IConfiguration configuration, IOptions<JwtBearerOptions> jwtBearerOptions)
    {
        _configuration = configuration;
        _jwtBearerOptions = jwtBearerOptions.Value;
    }

    public void Configure(AccessTokenServiceOptions options)
    {
        _configuration.GetRequiredSection("Auth:Generation:AccessTokens")
            .Bind(options);

        options.SecurityKey = _jwtBearerOptions.TokenValidationParameters.IssuerSigningKey;
    }
}