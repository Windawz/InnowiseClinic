using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class LogInService(
    IAccountService accountService,
    IAccessTokenService accessTokenService,
    IRefreshTokenService refreshTokenService,
    ILogInRequestMapperService logInRequestMapperService,
    ITokenResponseMapperService tokenResponseMapperService) : ILogInService
{
    private readonly IAccountService _accountService = accountService;
    private readonly IAccessTokenService _accessTokenService = accessTokenService;
    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;
    private readonly ILogInRequestMapperService _logInRequestMapperService = logInRequestMapperService;
    private readonly ITokenResponseMapperService _tokenResponseMapperService = tokenResponseMapperService;

    public async Task<TokenResponse> LogInAsync(LogInRequest request)
    {
        var (email, password) = _logInRequestMapperService.MapToEmailAndPassword(request);
        var account = await _accountService.AccessAccountAsync(email, password);
        var accessToken = await _accessTokenService.GenerateTokenAsync(account.Role);
        var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(account.Role);
        return _tokenResponseMapperService.MapFromTokenPair(accessToken, refreshToken);
    }
}
