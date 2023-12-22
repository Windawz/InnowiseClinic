using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class RegisterOtherService : IRegisterOtherService
{
    private readonly IRegisterOtherRequestMapperService _registerOtherRequestMapperService;
    private readonly IAccountService _accountService;

    public RegisterOtherService(
        IRegisterOtherRequestMapperService registerOtherRequestMapperService,
        IAccountService accountService)
    {
        _registerOtherRequestMapperService = registerOtherRequestMapperService;
        _accountService = accountService;
    }

    public async Task RegisterOther(RegisterOtherRequest request)
    {
        var (email, password, role) = _registerOtherRequestMapperService.MapToEmailPasswordRole(request);
        await _accountService.CreateAccountAsync(email, password, role);
    }
}