using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class TokenResponseMapperService(IRefreshTokenStringMapperService refreshTokenStringMapperService) : ITokenResponseMapperService
{
    public TokenResponse MapFromTokenPair(AccessToken accessToken, RefreshToken refreshToken)
    {
        return new()
        {
            AccessToken = accessToken.SignedValue,
            RefreshToken = refreshTokenStringMapperService.MapFromRefreshToken(refreshToken),
            TokenType = accessToken.TokenType,
            ExpiresInSeconds = (int)accessToken.ExpiresAt.Subtract(accessToken.CreatedAt).TotalSeconds,
        };
    }
}
