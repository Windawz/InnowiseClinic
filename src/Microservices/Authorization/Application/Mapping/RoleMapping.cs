using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Mapping;

public static class RoleMapping
{
    public static Role ToRole(string roleName)
    {
        if (!Enum.TryParse(roleName, ignoreCase: true, out Role role))
        {
            throw new ArgumentException(
                message: $"Unknown role name \"{roleName}\"",
                paramName: nameof(roleName));
        }

        return role;
    }

    public static string ToRoleName(Role role)
    {
        return Enum.GetName(role)?.ToLowerInvariant()
            ?? throw new ArgumentOutOfRangeException(
                message: $"Role constant with value {role} is not a valid role",
                paramName: nameof(role));
    }
}