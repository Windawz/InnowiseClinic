using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class RefreshService : IRefreshService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRefreshRequestMapperService _refreshRequestMapperService;
    private readonly ITokenResponseMapperService _tokenResponseMapperService;

    public RefreshService(
        IAccessTokenService accessTokenService,
        IRefreshTokenService refreshTokenService,
        IRefreshRequestMapperService refreshRequestMapperService,
        ITokenResponseMapperService tokenResponseMapperService)
    {
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
        _refreshRequestMapperService = refreshRequestMapperService;
        _tokenResponseMapperService = tokenResponseMapperService;
    }

    public async Task<TokenResponse> RefreshAsync(RefreshRequest request)
    {
        var refreshToken = _refreshRequestMapperService.MapToRefreshToken(request);
        var newRefreshToken = await _refreshTokenService.CreateReplacementRefreshTokenAsync(refreshToken);
        var accessToken = await _accessTokenService.GenerateTokenAsync(newRefreshToken.Role);
        return _tokenResponseMapperService.MapFromTokenPair(accessToken, newRefreshToken);
    }
}