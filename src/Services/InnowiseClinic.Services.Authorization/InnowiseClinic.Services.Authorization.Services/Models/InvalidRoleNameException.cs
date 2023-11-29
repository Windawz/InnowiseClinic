namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Thrown when no existing role is found for the given name.
/// </summary>
public class InvalidRoleNameException : BusinessModelException
{
    /// <summary>
    /// Creates an instance of <see cref="InvalidRoleNameException"/>.
    /// </summary>
    /// <param name="roleName">Name of the role that a matching role wasn't found for.</param>
    public InvalidRoleNameException(string roleName) : base($"No known role exists named \"{roleName}\"")
    {
        RoleName = roleName;
    }

    /// <summary>
    /// Name of the role that a matching role wasn't found for.
    /// </summary>
    public string RoleName { get; }
}