using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class RegisterOtherRequestMapperService : IRegisterOtherRequestMapperService
{
    private readonly IRoleNameMapperService _roleNameMapperService;

    public RegisterOtherRequestMapperService(IRoleNameMapperService roleNameMapperService)
    {
        _roleNameMapperService = roleNameMapperService;
    }

    public (string Email, string Password, Role Role) MapToEmailPasswordRole(RegisterOtherRequest request)
    {
        return (
            Email: request.Email.Trim(),
            Password: request.Password.Trim(),
            Role: _roleNameMapperService.MapToRole(request.Role.Trim()));
    }
}