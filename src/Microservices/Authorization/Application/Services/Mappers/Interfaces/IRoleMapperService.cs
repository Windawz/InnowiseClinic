using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;

public interface IRoleMapperService
{
    /// <exception cref="InvalidOperationException"/>
    string MapToRoleName(Role role);

    /// <exception cref="UnknownRoleException"/>
    Role MapFromRoleName(string roleName);
}