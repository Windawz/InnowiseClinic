using InnowiseClinic.Services.Authorization.API.Attributes.Debugging;
using InnowiseClinic.Services.Authorization.API.DataTransfer.Input;
using InnowiseClinic.Services.Authorization.API.Mapping.DataTransferToModels;
using InnowiseClinic.Services.Authorization.API.Services.Interfaces;
using InnowiseClinic.Services.Authorization.Services.Models;
using InnowiseClinic.Services.Authorization.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Services.Authorization.API.Controllers.Debugging;

[Route("god")]
[ApiController]
[DebugController]
public class GodController : ControllerBase
{
    private readonly IRegistrator _registrator;
    private readonly ITokenPairFactory _tokenFactory;
    private readonly IAccountRepository _repository;

    public GodController(IRegistrator registrator, ITokenPairFactory tokenFactory, IAccountRepository repository)
    {
        _registrator = registrator;
        _tokenFactory = tokenFactory;
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost("register/self/{roleName}")]
    public IActionResult RegisterSelfAsRole(RegisterSelfInput input, string roleName)
    {
        return Ok(
            _tokenFactory.Create(
                _registrator.RegisterSelf(
                    new Email(input.EmailAddress),
                    new Password(input.PasswordText),
                    new Role(roleName)))
                .MapToAPILayer());
    }

    [AllowAnonymous]
    [HttpGet("query/id/{id}")]
    public IActionResult QueryById(int id)
    {
        if (_repository.TryGetById(id, out var account))
        {
            return Ok(new
            {
                account.Id,
                Email = account.Email.Address,
                Password = account.Password.Text,
                CreatedBy = account.CreatedBy?.Id,
                UpdatedBy = account.UpdatedBy?.Id,
            });
        }
        else
        {
            return NotFound();
        }
    }
}