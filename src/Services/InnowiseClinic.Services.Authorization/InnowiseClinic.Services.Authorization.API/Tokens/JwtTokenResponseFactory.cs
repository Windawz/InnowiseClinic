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
    private const string _tokenSigningAlgorithm = SecurityAlgorithms.Sha256;
    private static readonly TimeSpan _accessTokenExpirationTime = TimeSpan.FromSeconds(30);
    private static readonly TimeSpan _refreshTokenExpirationTime = TimeSpan.FromHours(6);
    private readonly JwtBearerOptions _jwtBearerOptions;

    /// <summary>
    /// Creates an instance of <see cref="JwtTokenResponseFactory"/>.
    /// </summary>
    /// <param name="jwtBearerOptions">JWT bearer authentication options.</param>
    public JwtTokenResponseFactory(IOptionsMonitor<JwtBearerOptions> jwtBearerOptions)
    {
        _jwtBearerOptions = jwtBearerOptions.Get(JwtBearerDefaults.AuthenticationScheme);
    }

    /// <inheritdoc/>
    public TokenResponse Create(Account account)
    {
        var currentDateTime = DateTime.UtcNow;

        var accessToken = CreateAccessToken(account, currentDateTime);
        var refreshToken = CreateRefreshToken(account, currentDateTime);

        var tokenHandler = new JwtSecurityTokenHandler();

        var signedAccessToken = tokenHandler.WriteToken(accessToken);
        var signedRefreshToken = tokenHandler.WriteToken(refreshToken);

        return new TokenResponse(
            signedAccessToken,
            signedRefreshToken,
            JwtBearerDefaults.AuthenticationScheme,
            _accessTokenExpirationTime);
    }

    private JwtSecurityToken CreateAccessToken(
        Account account,
        DateTime currentDateTime)
    {
        return new JwtSecurityToken(
            issuer: _jwtBearerOptions.TokenValidationParameters.ValidIssuer,
            audience: _jwtBearerOptions.TokenValidationParameters.ValidAudience,
            expires: currentDateTime.Add(_accessTokenExpirationTime),
            notBefore: currentDateTime,
            signingCredentials: new SigningCredentials(
                key: _jwtBearerOptions.TokenValidationParameters.IssuerSigningKey,
                algorithm: _tokenSigningAlgorithm),
            claims: account.Roles
                .Select(role => new Claim(ClaimTypes.Role, role.Name))
                .Append(CreateSubjectClaim(account)));
    }

    private JwtSecurityToken CreateRefreshToken(Account account, DateTime currentDateTime)
    {
        return new JwtSecurityToken(
            issuer: _jwtBearerOptions.TokenValidationParameters.ValidIssuer,
            audience: _jwtBearerOptions.TokenValidationParameters.ValidAudience,
            expires: currentDateTime.Add(_refreshTokenExpirationTime),
            notBefore: currentDateTime,
            signingCredentials: new SigningCredentials(
                key: _jwtBearerOptions.TokenValidationParameters.IssuerSigningKey,
                algorithm: _tokenSigningAlgorithm),
            claims: Enumerable.Empty<Claim>()
                .Append(CreateSubjectClaim(account)));
    }

    private static Claim CreateSubjectClaim(Account account)
    {
        return new(JwtRegisteredClaimNames.Sub, account.Id.ToString());
    }
}