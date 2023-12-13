using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Implementations;

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
            throw new ArgumentException(
                message: $"Unknown role name \"{roleName}\"",
                paramName: nameof(roleName));
        }
    }

    public string MapToRoleName(Role role)
    {
        return Enum.GetName(role)?.ToLowerInvariant()
            ?? throw new ArgumentOutOfRangeException(
                message: $"Role constant with value {role} is not a valid role",
                paramName: nameof(role));
    }
}
