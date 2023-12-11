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
    ILogInResponseMapperService logInResponseMapperService) : ILogInService
{
    private readonly IAccountService _accountService = accountService;
    private readonly IAccessTokenService _accessTokenService = accessTokenService;
    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;
    private readonly ILogInResponseMapperService _logInResponseMapperService = logInResponseMapperService;

    public async Task<TokenResponse> LogInAsync(LogInRequest request)
    {
        var account = await _accountService.AccessAccountAsync(request.Email, request.Password);
        var accessToken = await _accessTokenService.GenerateTokenAsync(account.Role);
        var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(account.Role);
        return _logInResponseMapperService.MapToLogInResponse(accessToken, refreshToken);
    }
}
