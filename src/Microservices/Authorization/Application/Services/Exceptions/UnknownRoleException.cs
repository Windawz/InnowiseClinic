namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class UnknownRoleException : Exception
{
    public UnknownRoleException(string roleName)
    {
        RoleName = roleName;
    }

    public string RoleName { get; }

    public override string Message =>
        $"Unknown role with name \"{RoleName}\"";
}