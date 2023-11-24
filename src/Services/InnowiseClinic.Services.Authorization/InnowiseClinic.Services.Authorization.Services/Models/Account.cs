namespace InnowiseClinic.Services.Authorization.Services.Models;
/// <summary>
/// A user account.
/// </summary>
public record Account
{
    /// <summary>
    /// Creates an instance of <see cref="Account"/>.
    /// </summary>
    /// <param name="id">A unique identifier.</param>
    /// <param name="email">An email address.</param>
    /// <param name="password">A password.</param>
    /// <param name="roles">Roles to assign to the account.</param>
    public Account(int id, Email email, string password, params Role[] roles) : this(id, email, password, (IReadOnlyCollection<Role>)roles) { }

    /// <summary>
    /// Creates an instance of <see cref="Account"/>.
    /// </summary>
    /// <param name="id">A unique identifier.</param>
    /// <param name="email">An email address.</param>
    /// <param name="password">A password.</param>
    /// <param name="roles">Roles to assign to the account.</param>
    public Account(int id, Email email, string password, IReadOnlyCollection<Role> roles)
    {
        Id = id;
        Email = email;
        Password = password;
        Roles = roles;
    }

    /// <summary>
    /// The unique identifier of the account.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// The email of the account. 
    /// </summary>
    public Email Email { get; init; }

    /// <summary>
    /// The password of the account.
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// Whether the user of this account has verified their email or not.
    /// </summary>
    /// <remarks>
    /// False by default.
    /// </remarks>
    public bool IsEmailVerified { get; init; } = false;

    /// <summary>
    /// The account responsible for the creation of this account.
    /// </summary>
    public Account? CreatedBy { get; init; }

    /// <summary>
    /// When the account was created.
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Who has updated this account last.
    /// </summary>
    /// <remarks>
    /// Null if no updates have been made yet.
    /// </remarks>
    public Account? UpdatedBy { get; init; }

    /// <summary>
    /// When the account was last updated.
    /// </summary>
    /// <remarks>
    /// Null if no updates have been made yet.
    /// </remarks>
    public DateTime? UpdatedAt { get; init; }

    /// <summary>
    /// Roles that this account belongs to.
    /// </summary>
    public IReadOnlyCollection<Role> Roles { get; init; }
}