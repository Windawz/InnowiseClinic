using System.IdentityModel.Tokens.Jwt;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Options;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class AccessTokenService(
    JwtSecurityTokenHandler jwtSecurityTokenHandler,
    IRoleMapperService roleMapperService,
    IOptions<AccessTokenServiceOptions> options) : IAccessTokenService
{
    private const string _tokenType = "Bearer";
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
    private readonly IRoleMapperService _roleMapperService = roleMapperService;
    private readonly AccessTokenServiceOptions _options = options.Value;

    public async Task<AccessToken> GenerateTokenAsync(Role role)
    {
        var now = DateTime.UtcNow;

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: now.AddSeconds(_options.ExpirationSeconds),
            notBefore: now,
            signingCredentials: new SigningCredentials(
                key: _options.SecurityKey,
                algorithm: _options.Algorithm),
            claims:
            [
                new(_options.RoleClaimType, _roleMapperService.MapToRoleName(role)),
            ]);

        var signedToken = _jwtSecurityTokenHandler.WriteToken(token);

        await Task.CompletedTask;

        return new AccessToken(
            SignedValue: signedToken,
            CreatedAt: now,
            ExpiresAt: now.AddSeconds(_options.ExpirationSeconds),
            TokenType: _tokenType
        );
    }
}