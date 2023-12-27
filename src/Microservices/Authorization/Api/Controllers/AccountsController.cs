using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Authorization.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase
{
    private readonly ILogInService _logInService;
    private readonly IRegisterService _registerService;
    private readonly IRegisterOtherService _registerOtherService;
    private readonly IRefreshService _refreshService;

    public AccountsController(
        ILogInService logInService,
        IRegisterService registerService,
        IRegisterOtherService registerOtherService,
        IRefreshService refreshService)
    {
        _logInService = logInService;
        _registerService = registerService;
        _registerOtherService = registerOtherService;
        _refreshService = refreshService;
    }

    [HttpPost("login")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LogIn(LogInRequest request)
    {
        var response = await _logInService.LogInAsync(request);
        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _registerService.RegisterAsync(request);
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
        await _registerOtherService.RegisterOther(request);
        return Created();
    }

    [HttpPost("refresh")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var response = await _refreshService.RefreshAsync(request);
        return Ok(response);
    }
}