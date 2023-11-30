using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InnowiseClinic.Services.Authorization.Services.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Services.Authorization.API.Tokens;

/// <summary>
/// Produces instances of <see cref="TokenResponse"/> using JSON web tokens.
/// </summary>
internal class JwtTokenResponseFactory : ITokenResponseFactory
{
    private const string _tokenSigningAlgorithm = SecurityAlgorithms.HmacSha256Signature;
    private readonly JwtBearerOptions _jwtBearerOptions;
    private readonly JwtOptions _jwtGenerationOptions;

    /// <summary>
    /// Creates an instance of <see cref="JwtTokenResponseFactory"/>.
    /// </summary>
    /// <param name="jwtBearerOptions">JWT bearer authentication options.</param>
    public JwtTokenResponseFactory(
        IOptionsMonitor<JwtBearerOptions> jwtBearerOptions,
        IOptionsMonitor<JwtOptions> jwtGenerationOptions,
        ILogger<JwtTokenResponseFactory> logger)
    {
        _jwtBearerOptions = jwtBearerOptions.Get(JwtBearerDefaults.AuthenticationScheme);
        _jwtGenerationOptions = jwtGenerationOptions.CurrentValue;
    }

    /// <inheritdoc/>
    public TokenResponse Create(Account account)
    {
        var currentDateTime = DateTime.UtcNow;

        var accessTokenExpirationTime = TimeSpan.FromSeconds(_jwtGenerationOptions.Generation.AccessTokenExpirationSeconds);
        var refreshTokenExpirationTime = TimeSpan.FromSeconds(_jwtGenerationOptions.Generation.RefreshTokenExpirationSeconds);

        var accessToken = CreateAccessToken(account, currentDateTime, accessTokenExpirationTime);
        var refreshToken = CreateRefreshToken(account, currentDateTime, refreshTokenExpirationTime);

        var tokenHandler = new JwtSecurityTokenHandler();

        var signedAccessToken = tokenHandler.WriteToken(accessToken);
        var signedRefreshToken = tokenHandler.WriteToken(refreshToken);

        var response = new TokenResponse(
            signedAccessToken,
            signedRefreshToken,
            JwtBearerDefaults.AuthenticationScheme,
            accessTokenExpirationTime);

        return response;
    }

    private JwtSecurityToken CreateAccessToken(
        Account account,
        DateTime currentDateTime,
        TimeSpan expirationTime)
    {
        return new JwtSecurityToken(
            issuer: _jwtBearerOptions.TokenValidationParameters.ValidIssuer,
            audience: _jwtBearerOptions.TokenValidationParameters.ValidAudience,
            expires: currentDateTime.Add(expirationTime),
            notBefore: currentDateTime,
            signingCredentials: new SigningCredentials(
                key: _jwtBearerOptions.TokenValidationParameters.IssuerSigningKey,
                algorithm: _tokenSigningAlgorithm),
            claims: CreateBaseTokenClaims(account)
                .Concat(CreateAccessTokenClaims(account)));
    }

    private JwtSecurityToken CreateRefreshToken(
        Account account,
        DateTime currentDateTime,
        TimeSpan expirationTime)
    {
        return new JwtSecurityToken(
            issuer: _jwtBearerOptions.TokenValidationParameters.ValidIssuer,
            audience: _jwtBearerOptions.TokenValidationParameters.ValidAudience,
            expires: currentDateTime.Add(expirationTime),
            notBefore: currentDateTime,
            signingCredentials: new SigningCredentials(
                key: _jwtBearerOptions.TokenValidationParameters.IssuerSigningKey,
                algorithm: _tokenSigningAlgorithm),
            claims: CreateBaseTokenClaims(account)
                .Concat(CreateRefreshTokenClaims(account)));
    }

    private static IEnumerable<Claim> CreateAccessTokenClaims(Account account)
    {
        return new Claim[]
        {
            new(ClaimTypes.Role, account.Role.Name),
        };
    }

    private static IEnumerable<Claim> CreateRefreshTokenClaims(Account account)
    {
        return Enumerable.Empty<Claim>();
    }

    private static IEnumerable<Claim> CreateBaseTokenClaims(Account account)
    {
        return new Claim[]
        {
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
        };
    }
}