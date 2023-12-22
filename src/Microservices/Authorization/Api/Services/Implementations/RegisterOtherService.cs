using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class RegisterOtherService(
    IRegisterOtherRequestMapperService registerOtherRequestMapperService,
    IAccountService accountService) : IRegisterOtherService
{
    public async Task RegisterOther(RegisterOtherRequest request)
    {
        var (email, password, role) = registerOtherRequestMapperService.MapToEmailPasswordRole(request);
        await accountService.CreateAccountAsync(email, password, role);
    }
}