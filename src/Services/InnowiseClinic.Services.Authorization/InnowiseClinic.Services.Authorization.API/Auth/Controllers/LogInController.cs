using System.Security.Claims;
using InnowiseClinic.Services.Authorization.API.Auth.Binding.Attributes;
using InnowiseClinic.Services.Authorization.API.Auth.DataTransfer.Input;
using InnowiseClinic.Services.Authorization.API.Auth.Mapping;
using InnowiseClinic.Services.Authorization.API.Auth.Services.Interfaces;
using InnowiseClinic.Services.Authorization.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Services.Authorization.API.Auth.Controllers;

[ApiController]
[Route("login")]
public class LogInController : ControllerBase
{
    private readonly ILogInAccessor _accessor;
    private readonly IResolver _resolver;
    private readonly ITokenPairFactory _tokenFactory;

    public LogInController(ILogInAccessor accessor, IResolver resolver, ITokenPairFactory tokenFactory)
    {
        _accessor = accessor;
        _resolver = resolver;
        _tokenFactory = tokenFactory;
    }

    [AllowAnonymous]
    [HttpGet("credentials")]
    public IActionResult LogInWithCredentials(LogInInput input)
    {
        var (email, password) = input.MapToServiceLayer();
        return Ok(_tokenFactory.Create(
            _accessor.GetAccess(
                email,
                password))
            .MapToAPILayer());
    }

    [Authorize]
    [HttpGet("refresh")]
    public IActionResult LogInWithToken([FromClaims(ClaimTypes.NameIdentifier)] int accountId)
    {
        return Ok(
            _tokenFactory.Create(_resolver.Resolve(accountId))
                .MapToAPILayer());
    }
}