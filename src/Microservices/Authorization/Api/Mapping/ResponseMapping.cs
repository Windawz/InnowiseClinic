using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Mapping;

public static class ResponseMapping
{
    public static TokenResponse ToTokenResponse(AccessToken accessToken, RefreshToken refreshToken)
    {
        return new()
        {
            AccessToken = accessToken.SignedValue,
            RefreshToken = RefreshTokenStringMapping.ToString(refreshToken),
            TokenType = accessToken.TokenType,
            ExpiresInSeconds = (int)accessToken.ExpiresAt.Subtract(accessToken.CreatedAt).TotalSeconds,
        };
    }
}