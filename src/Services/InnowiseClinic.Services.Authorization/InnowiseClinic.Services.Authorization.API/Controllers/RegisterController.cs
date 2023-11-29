using InnowiseClinic.Services.Authorization.API.Models;
using InnowiseClinic.Services.Authorization.API.Tokens;
using InnowiseClinic.Services.Authorization.Services;
using InnowiseClinic.Services.Authorization.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace InnowiseClinic.Services.Authorization.API.Controllers;

[ApiController]
[Route("register")]
public class RegisterController : ControllerBase
{
    private readonly IRegistrator _registrator;
    private readonly IResolver _resolver;
    private readonly ITokenResponseFactory _responseFactory;
    private readonly ILogger _logger;

    public RegisterController(
        IRegistrator registrator,
        IResolver resolver,
        ITokenResponseFactory responseFactory,
        ILogger<RegisterController> logger)
    {
        _registrator = registrator;
        _resolver = resolver;
        _responseFactory = responseFactory;
        _logger = logger;
    }

    [Authorize(Roles = RoleNames.Receptionist)]
    [HttpPost("other")]
    public IActionResult RegisterOther(RegisterOtherInput input)
    {
        _registrator.RegisterOther(
            _resolver.Resolve(input.InitiatorId),
            new Email(input.EmailAddress),
            new Password(input.PasswordText),
            new Role(input.RoleName));
            
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("self")]
    public IActionResult RegisterSelf(RegisterSelfInput input)
    {
        return Ok(
            _responseFactory.Create(
                _registrator.RegisterSelf(
                    new Email(input.EmailAddress),
                    new Password(input.PasswordText))));
    }
}