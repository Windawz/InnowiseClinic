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
    public async Task<TokenResponse> LogInAsync(LogInRequest request)
    {
        var (email, password) = logInRequestMapperService.MapToEmailAndPassword(request);
        var account = await accountService.AccessAccountAsync(email, password);
        var accessToken = await accessTokenService.GenerateTokenAsync(account.Role);
        var refreshToken = await refreshTokenService.CreateRefreshTokenAsync(account.Role);
        return tokenResponseMapperService.MapFromTokenPair(accessToken, refreshToken);
    }
}
