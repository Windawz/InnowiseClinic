using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Mapping;
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

    public AccountsController(
        IAccountService accountService,
        IAccessTokenService accessTokenService,
        IRefreshTokenService refreshTokenService)
    {
        _accountService = accountService;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    [HttpPost("login")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LogIn(LogInRequest request)
    {
        var (email, password) = RequestMapping.ToEmailPassword(request);
        var account = await _accountService.AccessAccountAsync(email, password);
        var accessToken = await _accessTokenService.GenerateTokenAsync(account.Role);
        var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(account.Role);
        var response = ResponseMapping.ToTokenResponse(accessToken, refreshToken);
        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var (email, password) = RequestMapping.ToEmailPassword(request);
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
        var (email, password, role) = RequestMapping.ToEmailPasswordRole(request);
        await _accountService.CreateAccountAsync(email, password, role);
        return Created();
    }

    [HttpPost("refresh")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var refreshToken = RequestMapping.ToRefreshToken(request);
        var newRefreshToken = await _refreshTokenService.RecreateRefreshTokenAsync(refreshToken);
        var accessToken = await _accessTokenService.GenerateTokenAsync(newRefreshToken.Role);
        var response = ResponseMapping.ToTokenResponse(accessToken, newRefreshToken);
        return Ok(response);
    }
}