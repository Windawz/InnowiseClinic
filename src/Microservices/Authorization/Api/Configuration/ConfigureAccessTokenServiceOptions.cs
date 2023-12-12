using InnowiseClinic.Microservices.Authorization.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Api.Configuration;

public class ConfigureAccessTokenServiceOptions(
    IConfiguration configuration,
    IOptions<JwtBearerOptions> jwtBearerOptions) : IConfigureOptions<AccessTokenServiceOptions>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly JwtBearerOptions _jwtBearerOptions = jwtBearerOptions.Value;

    public void Configure(AccessTokenServiceOptions options)
    {
        _configuration.GetRequiredSection("AccessTokens")
            .Bind(options);

        options.SecurityKey = _jwtBearerOptions.TokenValidationParameters.IssuerSigningKey;
    }
}