using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Services.Authorization.API.Options.Configurators.Jwt;

public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    public ConfigureJwtBearerOptions(IOptionsMonitor<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.CurrentValue;
    }

    public void Configure(string name, JwtBearerOptions options)
    {
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new()
        {
            ValidAudience = _jwtOptions.Audience,
            ValidIssuer = _jwtOptions.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _jwtOptions.SigningKey)),
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(5),
        };
    }
}