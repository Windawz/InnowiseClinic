using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Shared.Api.Constants;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class RoleNameMapperService : IRoleNameMapperService
{
    private static readonly IReadOnlyDictionary<Role, string> _roleNamesToRoles = new Dictionary<Role, string>()
    {
        [Role.Patient] = RoleName.Patient,
        [Role.Doctor] = RoleName.Doctor,
        [Role.Receptionist] = RoleName.Receptionist,
    };

    private static readonly IReadOnlyDictionary<string, Role> _rolesToRoleNames = _roleNamesToRoles
        .ToDictionary(
            keySelector: kv => kv.Value,
            elementSelector: kv => kv.Key,
            comparer: StringComparer.OrdinalIgnoreCase);

    public string MapFromRole(Role role)
    {
        if (_roleNamesToRoles.TryGetValue(role, out var roleName))
        {
            return roleName;
        }
        else
        {
            throw new ArgumentOutOfRangeException(
                message: $"Invalid {nameof(Role)} value {role}",
                paramName: nameof(role));
        }
    }

    public Role MapToRole(string roleName)
    {
        if (_rolesToRoleNames.TryGetValue(roleName, out var role))
        {
            return role;
        }
        else
        {
            throw new ArgumentOutOfRangeException(
                message: $"Invalid {nameof(RoleName)} constant {roleName}",
                paramName: nameof(roleName));
        }
    }
}