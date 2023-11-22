namespace InnowiseClinic.Services.Authorization.Data.Models;

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
    /// Whether a user with an account having this role can register new
    /// accounts or not.
    /// </summary>
    /// // TODO: Keep track of a list of roles that are registerable by this role instead.
    public bool CanRegisterOthers { get; set; }

    /// <summary>
    /// A navigation over accounts belonging to the role.
    /// </summary>
    public ICollection<Account> Accounts { get; } = new List<Account>();
}