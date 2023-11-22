using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    public JwtTokenResponseFactory(IOptions<JwtBearerOptions> jwtBearerOptions)
    {
        _jwtBearerOptions = jwtBearerOptions.Value;
    }

    /// <inheritdoc/>
    public TokenResponse Create(int accountId, IReadOnlyCollection<string> accountRoleNames)
    {
        var currentDateTime = DateTime.Now;

        var accessToken = CreateAccessToken(accountId, accountRoleNames, currentDateTime);
        var refreshToken = CreateRefreshToken(accountId, currentDateTime);

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
        int accountId,
        IReadOnlyCollection<string> accountRoleNames,
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
            claims: accountRoleNames
                .Select(roleName => new Claim(ClaimTypes.Role, roleName))
                .Append(CreateSubjectClaim(accountId)));
    }

    private JwtSecurityToken CreateRefreshToken(int accountId, DateTime currentDateTime)
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
                .Append(CreateSubjectClaim(accountId)));
    }

    private static Claim CreateSubjectClaim(int accountId)
    {
        return new(JwtRegisteredClaimNames.Sub, accountId.ToString());
    }
}