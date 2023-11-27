using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

public class NotPermittedToAssignRoleException : BusinessLogicException
{
    public NotPermittedToAssignRoleException(Account initiator, Email accountEmail, IReadOnlyCollection<Role> roles)
        : base(CreateMessage(initiator, accountEmail, roles))
    {
        Initiator = initiator;
        AccountEmail = accountEmail;
        Roles = roles;
    }

    public Account Initiator { get; }
    
    public Email AccountEmail { get; }

    public IReadOnlyCollection<Role> Roles { get; }

    private static string CreateMessage(Account initiator, Email accountEmail, IReadOnlyCollection<Role> roles)
    {
        string stringifiedRoles = string.Join(", ", roles);
        return $"Unpermitted attempt initiated by {initiator}"
        + $" to assign roles [{stringifiedRoles}] to account being registered"
        + $" with email {accountEmail}";
    }
}