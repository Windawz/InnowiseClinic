namespace InnowiseClinic.Services.Authorization.Data.Models;

/// <summary>
/// Represents a user role.
/// </summary>
public class Role
{
    /// <summary>
    /// A unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the role.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// A navigation over accounts belonging to the role.
    /// </summary>
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}