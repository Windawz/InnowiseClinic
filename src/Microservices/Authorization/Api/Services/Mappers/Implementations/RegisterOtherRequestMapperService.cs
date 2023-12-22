using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class RegisterOtherRequestMapperService(IRoleMapperService roleMapperService) : IRegisterOtherRequestMapperService
{
    public (string Email, string Password, Role Role) MapToEmailPasswordRole(RegisterOtherRequest request)
    {
        return (
            Email: request.Email.Trim(),
            Password: request.Password.Trim(),
            Role: roleMapperService.MapFromRoleName(request.Role.Trim()));
    }
}