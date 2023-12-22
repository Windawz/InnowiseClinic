using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class RegisterService : IRegisterService
{
    private readonly IRegisterRequestMapperService _registerRequestMapperService;
    private readonly IAccountService _accountService;

    public RegisterService(
        IRegisterRequestMapperService registerRequestMapperService,
        IAccountService accountService)
    {
        _registerRequestMapperService = registerRequestMapperService;
        _accountService = accountService;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var (email, password) = _registerRequestMapperService.MapToEmailPassword(request);
        await _accountService.CreateAccountAsync(email, password, Role.Patient);
    }
}
