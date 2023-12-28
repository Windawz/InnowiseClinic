using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Authorization.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IAccessTokenService _accessTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ILogInRequestMapperService _logInRequestMapperService;
    private readonly IRegisterRequestMapperService _registerRequestMapperService;
    private readonly IRegisterOtherRequestMapperService _registerOtherRequestMapperService;
    private readonly IRefreshRequestMapperService _refreshRequestMapperService;
    private readonly ITokenResponseMapperService _tokenResponseMapperService;

    public AccountsController(
        IAccountService accountService,
        IAccessTokenService accessTokenService,
        IRefreshTokenService refreshTokenService,
        ILogInRequestMapperService logInRequestMapperService,
        IRegisterRequestMapperService registerRequestMapperService,
        IRegisterOtherRequestMapperService registerOtherRequestMapperService,
        IRefreshRequestMapperService refreshRequestMapperService,
        ITokenResponseMapperService tokenResponseMapperService)
    {
        _accountService = accountService;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
        _logInRequestMapperService = logInRequestMapperService;
        _registerRequestMapperService = registerRequestMapperService;
        _registerOtherRequestMapperService = registerOtherRequestMapperService;
        _refreshRequestMapperService = refreshRequestMapperService;
        _tokenResponseMapperService = tokenResponseMapperService;
    }

    [HttpPost("login")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LogIn(LogInRequest request)
    {
        var (email, password) = _logInRequestMapperService.MapToEmailAndPassword(request);
        var account = await _accountService.AccessAccountAsync(email, password);
        var accessToken = await _accessTokenService.GenerateTokenAsync(account.Role);
        var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(account.Role);
        var response = _tokenResponseMapperService.MapFromTokenPair(accessToken, refreshToken);
        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var (email, password) = _registerRequestMapperService.MapToEmailPassword(request);
        await _accountService.CreateAccountAsync(email, password, Role.Patient);
        return Created();
    }

    [Authorize(Roles = RoleName.Receptionist)]
    [HttpPost("register/other")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterOther(RegisterOtherRequest request)
    {
        var (email, password, role) = _registerOtherRequestMapperService.MapToEmailPasswordRole(request);
        await _accountService.CreateAccountAsync(email, password, role);
        return Created();
    }

    [HttpPost("refresh")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var refreshToken = _refreshRequestMapperService.MapToRefreshToken(request);
        var newRefreshToken = await _refreshTokenService.CreateReplacementRefreshTokenAsync(refreshToken);
        var accessToken = await _accessTokenService.GenerateTokenAsync(newRefreshToken.Role);
        var response = _tokenResponseMapperService.MapFromTokenPair(accessToken, newRefreshToken);
        return Ok(response);
    }
}