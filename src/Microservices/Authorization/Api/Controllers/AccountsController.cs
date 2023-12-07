using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Authorization.Api.Controllers;

[ApiController]
public class AccountsController : ControllerBase
{
    [HttpPost("login")]
    public LogInResponse LogIn(LogInRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public IActionResult Regitster(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpGet("refresh")]
    public RefreshResponse Refresh(RefreshRequest request)
    {
        throw new NotImplementedException();
    }
}