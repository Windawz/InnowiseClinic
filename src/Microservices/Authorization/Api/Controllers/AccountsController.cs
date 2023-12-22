using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Authorization.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController(
    ILogInService logInService,
    IRegisterService registerService,
    IRegisterOtherService registerOtherService,
    IRefreshService refreshService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<TokenResponse> LogIn(LogInRequest request)
    {
        return await logInService.LogInAsync(request);
    }

    [HttpPost("register")]
    public async Task Register(RegisterRequest request)
    {
        await registerService.RegisterAsync(request);
    }

    [Authorize(Roles = "receptionist")]
    [HttpPost("register/other")]
    public async Task RegisterOther(RegisterOtherRequest request)
    {
        await registerOtherService.RegisterOther(request);
    }

    [HttpPost("refresh")]
    public async Task<TokenResponse> Refresh(RefreshRequest request)
    {
        return await refreshService.RefreshAsync(request);
    }
}