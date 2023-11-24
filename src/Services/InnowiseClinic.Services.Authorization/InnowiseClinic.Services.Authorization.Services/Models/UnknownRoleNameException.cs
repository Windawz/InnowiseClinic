namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Thrown when no existing role is found for the given name.
/// </summary>
public class UnknownRoleNameException : ModelValidationException
{
    /// <summary>
    /// Creates an instance of <see cref="UnknownRoleNameException"/>.
    /// </summary>
    /// <param name="roleName">Name of the role that a matching role wasn't found for.</param>
    public UnknownRoleNameException(string roleName) : base($"No known role exists named \"{roleName}\"")
    {
        RoleName = roleName;
    }

    /// <summary>
    /// Name of the role that a matching role wasn't found for.
    /// </summary>
    public string RoleName { get; }
}