using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class RegisterService(
    IRegisterRequestMapperService registerRequestMapperService,
    IAccountService accountService) : IRegisterService
{
    private readonly IRegisterRequestMapperService _registerRequestMapperService = registerRequestMapperService;
    private readonly IAccountService _accountService = accountService;

    public async Task RegisterAsync(RegisterRequest request)
    {
        var (email, password, role) = _registerRequestMapperService.MapToEmailPasswordAndRole(request);
        await _accountService.CreateAccountAsync(email, password, role);
    }
}
