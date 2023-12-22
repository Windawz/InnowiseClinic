using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class TokenResponseMapperService : ITokenResponseMapperService
{
    private readonly IRefreshTokenStringMapperService _refreshTokenStringMapperService;

    public TokenResponseMapperService(IRefreshTokenStringMapperService refreshTokenStringMapperService)
    {
        _refreshTokenStringMapperService = refreshTokenStringMapperService;
    }

    public TokenResponse MapFromTokenPair(AccessToken accessToken, RefreshToken refreshToken)
    {
        return new()
        {
            AccessToken = accessToken.SignedValue,
            RefreshToken = _refreshTokenStringMapperService.MapFromRefreshToken(refreshToken),
            TokenType = accessToken.TokenType,
            ExpiresInSeconds = (int)accessToken.ExpiresAt.Subtract(accessToken.CreatedAt).TotalSeconds,
        };
    }
}
