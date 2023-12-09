using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Authorization.Api.Controllers;

[ApiController]
public class AccountsController : ControllerBase
{
    private readonly ILogInService _logInService;
    private readonly IRegisterService _registerService;
    private readonly IRefreshService _refreshService;

    public AccountsController(ILogInService logInService, IRegisterService registerService, IRefreshService refreshService)
    {
        _logInService = logInService;
        _registerService = registerService;
        _refreshService = refreshService;
    }

    [HttpPost("login")]
    public async Task<LogInResponse> LogIn(LogInRequest request)
    {
        return await _logInService.LogInAsync(request);
    }

    [HttpPost("register")]
    public async Task Register(RegisterRequest request)
    {
        await _registerService.RegisterAsync(request);
    }

    [HttpGet("refresh")]
    public async Task<RefreshResponse> Refresh(RefreshRequest request)
    {
        return await _refreshService.RefreshAsync(request);
    }
}