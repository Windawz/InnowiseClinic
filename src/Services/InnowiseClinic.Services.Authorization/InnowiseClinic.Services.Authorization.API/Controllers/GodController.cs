using InnowiseClinic.Services.Authorization.API.DataTransfer;
using InnowiseClinic.Services.Authorization.API.Models;
using InnowiseClinic.Services.Authorization.API.Services;
using InnowiseClinic.Services.Authorization.Services.Models;
using InnowiseClinic.Services.Authorization.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Services.Authorization.API.Controllers;

[Route("god")]
public class GodController : DevelopmentController
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
                .ToDataTransferTokenPair());
    }

    [AllowAnonymous]
    [HttpGet("query/id/{id}")]
    public IActionResult QueryById(int id)
    {
        if (_repository.TryGetById(id, out var account))
        {
            return Ok(new
            {
                Id = account.Id,
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