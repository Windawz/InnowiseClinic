using System.ComponentModel.DataAnnotations;

namespace InnowiseClinic.Services.Authorization.Data.Models;

/// <summary>
/// Represents a user account.
/// </summary>
public class Account
{
    /// <summary>
    /// A unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// An email address.
    /// </summary>
    /// <remarks>
    /// The string value must denote a valid email address.
    /// </remarks>
    [EmailAddress]
    public string Email { get; set; } = null!;

    /// <summary>
    /// A literal password string.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// A phone number.
    /// </summary>
    /// <remarks>
    /// The string value must denote a valid phone number.
    /// </remarks>
    [Phone]
    public string? PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Whether the email has been verified or not.
    /// </summary>
    public bool IsEmailVerified { get; set; }

    /// <summary>
    /// A unique identifier of the photo representing the account's user.
    /// </summary>
    public int? PhotoId { get; set; }

    /// <summary>
    /// The account that is responsible for creation of this account.
    /// </summary>
    /// <remarks>
    /// Null if this account has been created by the user that it represents.
    /// </remarks>
    public Account? CreatedBy { get; set; }

    /// <summary>
    /// A point in time denoting when the account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The account that is responsible for being the last one to update this account's information.
    /// </summary>
    /// <remarks>
    /// Equal to this account if it has been updated by the user it represents.
    /// <para/>
    /// Is null if the account has not been updated yet.
    /// </remarks>
    public Account? UpdatedBy { get; set; }

    /// <summary>
    /// A point in time denoting when the account was last updated.
    /// </summary>
    /// <remarks>
    /// Is null if <see cref="UpdatedBy"/> is null.
    /// </remarks>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// A navigation over roles assigned to the account.
    /// </summary>
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}