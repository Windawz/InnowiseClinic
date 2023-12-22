using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class RegisterService(
    IRegisterRequestMapperService registerRequestMapperService,
    IAccountService accountService) : IRegisterService
{
    public async Task RegisterAsync(RegisterRequest request)
    {
        var (email, password) = registerRequestMapperService.MapToEmailPassword(request);
        await accountService.CreateAccountAsync(email, password, Role.Patient);
    }
}
