using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Shared.Api.Constants;

namespace InnowiseClinic.Microservices.Authorization.Api.Mapping;

public static class RoleNameMapping
{
    private static readonly IReadOnlyDictionary<Role, string> _roleNameToRoleMap = new Dictionary<Role, string>()
    {
        [Role.Patient] = RoleName.Patient,
        [Role.Doctor] = RoleName.Doctor,
        [Role.Receptionist] = RoleName.Receptionist,
    };
    private static readonly IReadOnlyDictionary<string, Role> _roleToRoleNameMap = _roleNameToRoleMap
        .ToDictionary(
            keySelector: kv => kv.Value,
            elementSelector: kv => kv.Key,
            comparer: StringComparer.OrdinalIgnoreCase);

    public static string ToRoleName(Role role)
    {
        if (_roleNameToRoleMap.TryGetValue(role, out var roleName))
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

    public static Role ToRole(string roleName)
    {
        if (_roleToRoleNameMap.TryGetValue(roleName, out var role))
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