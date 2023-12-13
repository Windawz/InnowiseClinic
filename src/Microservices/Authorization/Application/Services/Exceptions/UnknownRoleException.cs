namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class UnknownRoleException(string roleName) : Exception
{
    public string RoleName { get; } = roleName;

    public override string Message =>
        $"Unknown role with name \"{RoleName}\"";
}