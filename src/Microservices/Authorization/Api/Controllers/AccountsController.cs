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
    public async Task<TokenResponse> LogIn(LogInRequest request)
    {
        return await _logInService.LogInAsync(request);
    }

    [HttpPost("register")]
    public async Task Register(RegisterRequest request)
    {
        await _registerService.RegisterAsync(request);
    }

    [Authorize(Roles = RoleName.Receptionist)]
    [HttpPost("register/other")]
    public async Task RegisterOther(RegisterOtherRequest request)
    {
        await _registerOtherService.RegisterOther(request);
    }

    [HttpPost("refresh")]
    public async Task<TokenResponse> Refresh(RefreshRequest request)
    {
        return await _refreshService.RefreshAsync(request);
    }
}