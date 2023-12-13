using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class RegisterService(
    IRegisterRequestMapperService registerRequestMapperService,
    IAccountService accountService) : IRegisterService
{
    public async Task RegisterAsync(RegisterRequest request)
    {
        var (email, password, role) = registerRequestMapperService.MapToEmailPasswordAndRole(request);
        await accountService.CreateAccountAsync(email, password, role);
    }
}
