namespace InnowiseClinic.Services.Authorization.Services.Models;

public class InvalidRoleNameException : BusinessModelException
{
    public InvalidRoleNameException(string roleName) : base($"No known role exists named \"{roleName}\"")
    {
        RoleName = roleName;
    }

    public string RoleName { get; }
}