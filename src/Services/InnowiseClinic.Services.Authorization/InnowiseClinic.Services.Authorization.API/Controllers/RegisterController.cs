using InnowiseClinic.Services.Authorization.Services.Services;
using InnowiseClinic.Services.Authorization.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InnowiseClinic.Services.Authorization.API.Services;
using InnowiseClinic.Services.Authorization.API.DataTransfer;
using InnowiseClinic.Services.Authorization.API.Models;
using InnowiseClinic.Services.Authorization.API.Binding;
using System.Security.Claims;

namespace InnowiseClinic.Services.Authorization.API.Controllers;

[ApiController]
[Route("register")]
public class RegisterController : ControllerBase
{
    private readonly IRegistrator _registrator;
    private readonly IResolver _resolver;
    private readonly ITokenPairFactory _responseFactory;

    public RegisterController(
        IRegistrator registrator,
        IResolver resolver,
        ITokenPairFactory responseFactory)
    {
        _registrator = registrator;
        _resolver = resolver;
        _responseFactory = responseFactory;
    }

    [Authorize(Roles = RoleNames.Receptionist)]
    [HttpPost("other")]
    public IActionResult RegisterOther([FromClaims(ClaimTypes.NameIdentifier)] int initiatorId, RegisterOtherInput input)
    {
        var initiator = _resolver.Resolve(initiatorId);
        var (email, password, role) = input.MapToServiceLayer();

        _registrator.RegisterOther(initiator, email, password, role);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("self")]
    public IActionResult RegisterSelf(RegisterSelfInput input)
    {
        var (email, password) = input.MapToServiceLayer();

        return Ok(
            _responseFactory.Create(
                _registrator.RegisterSelf(
                    email,
                    password,
                    new Role(RoleNames.Patient)))
                .MapToAPILayer());
    }
}