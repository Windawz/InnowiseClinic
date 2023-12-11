using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class RoleMapperService : IRoleMapperService
{
    public Role MapFromRoleName(string roleName)
    {
        if (Enum.TryParse(roleName, ignoreCase: true, out Role role))
        {
            return role;
        }
        else
        {
            throw new UnknownRoleException(roleName);
        }
    }

    public string MapToRoleName(Role role)
    {
        return Enum.GetName(role)?.ToLowerInvariant()
            ?? throw new InvalidOperationException($"Role constant with value {role} is not a valid role");
    }
}
