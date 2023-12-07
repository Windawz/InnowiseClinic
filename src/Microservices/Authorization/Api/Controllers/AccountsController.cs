using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Authorization.Api.Controllers;

[ApiController]
public class AccountsController : ControllerBase
{
    [HttpPost("login")]
    public async Task<LogInResponse> LogIn(LogInRequest request)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Regitster(RegisterRequest request)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpGet("refresh")]
    public async Task<RefreshResponse> Refresh(RefreshRequest request)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}