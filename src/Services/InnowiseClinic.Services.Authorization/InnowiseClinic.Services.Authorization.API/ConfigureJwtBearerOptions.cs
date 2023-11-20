using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Services.Authorization.API;

/// <summary>
/// Configures JWT authentication.
/// </summary>
/// <remarks>
/// Valid audience, valid issuer, and the secret are
/// extracted from the current configuration.
/// <para/>
/// The secret is assumed to be a UTF8 string.
/// </remarks>
internal class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string ValidAudienceConfigurationKey = "Auth:Jwt:ValidAudience";
    private const string ValidIssuerConfigurationKey = "Auth:Jwt:ValidIssuer";
    private const string SecretConfigurationKey = "Auth:Jwt:Secret";
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Creates an instance of <see cref="ConfigureJwtBearerOptions"/>.
    /// </summary>
    /// <param name="configuration">An instance of <see cref="IConfiguration"/>.</param>
    public ConfigureJwtBearerOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public void Configure(string name, JwtBearerOptions options)
    {
        Configure(options);
    }

    /// <inheritdoc/>
    public void Configure(JwtBearerOptions options)
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new()
        {
            ValidAudience = _configuration[ValidAudienceConfigurationKey],
            ValidIssuer = _configuration[ValidIssuerConfigurationKey],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration[SecretConfigurationKey])),
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(5),
        };
    }
}