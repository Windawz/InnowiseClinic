using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface IRoleMapperService
{
    string MapToRoleName(Role role);
    Role MapFromRoleName(string roleName);
}