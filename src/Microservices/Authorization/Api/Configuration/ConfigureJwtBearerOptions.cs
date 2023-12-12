using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Microservices.Authorization.Api.Configuration;

public class ConfigureJwtBearerOptions(IConfiguration configuration) : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IConfiguration _configuration = configuration;

    private const string BaseConfigurationKey = "Auth:JwtBearer";
    private const bool DefaultValidateLifetimeValue = false;
    private const int DefaultClockSkewSecondsValue = 5;
    private const string NameClaimType = ClaimTypes.Name;
    private const string RoleClaimType = ClaimTypes.Role;

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        var section = _configuration.GetRequiredSection(BaseConfigurationKey);

        string? validAudience = section.GetValue<string>("ValidAudience");
        string? validIssuer = section.GetValue<string>("ValidIssuer");
        string? issuerSigningKey = section.GetValue<string>("IssuerSigningKey");
        bool validateLifetime = section.GetValue("ValidateLifetime", defaultValue: DefaultValidateLifetimeValue);
        TimeSpan clockSkew = TimeSpan.FromSeconds(
            section.GetValue("ClockSkewSeconds", defaultValue: DefaultClockSkewSecondsValue));

        var parameters = options.TokenValidationParameters;

        if (validAudience is not null)
        {
            options.Audience = validAudience;
            parameters.ValidAudience = validAudience;
            parameters.ValidateAudience = true;
        }
        else
        {
            parameters.ValidateAudience = false;
        }

        if (validIssuer is not null)
        {
            options.ClaimsIssuer = validIssuer;
            parameters.ValidIssuer = validIssuer;
            parameters.ValidateIssuer = true;
        }
        else
        {
            parameters.ValidateIssuer = false;
        }

        if (issuerSigningKey is not null)
        {
            parameters.IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(issuerSigningKey));
            
            parameters.ValidateIssuerSigningKey = true;
        }
        else
        {
            parameters.ValidateIssuerSigningKey = false;
        }
        
        parameters.ValidateLifetime = validateLifetime;
        parameters.ClockSkew = clockSkew;
        parameters.NameClaimType = NameClaimType;
        parameters.RoleClaimType = RoleClaimType;
    }
}