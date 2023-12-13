using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class RefreshService(
    IAccessTokenService accessTokenService,
    IRefreshTokenService refreshTokenService,
    IRefreshRequestMapperService refreshRequestMapperService,
    ITokenResponseMapperService tokenResponseMapperService) : IRefreshService
{
    public async Task<TokenResponse> RefreshAsync(RefreshRequest request)
    {
        var refreshToken = refreshRequestMapperService.MapToRefreshToken(request);
        var newRefreshToken = await refreshTokenService.CreateReplacementRefreshTokenAsync(refreshToken);
        var accessToken = await accessTokenService.GenerateTokenAsync(newRefreshToken.Role);
        return tokenResponseMapperService.MapFromTokenPair(accessToken, newRefreshToken);
    }
}