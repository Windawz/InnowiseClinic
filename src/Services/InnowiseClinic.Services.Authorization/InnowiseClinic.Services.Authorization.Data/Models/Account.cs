using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnowiseClinic.Services.Authorization.Data.Models;

/// <summary>
/// Represents a user account.
/// </summary>
public class Account
{
    /// <summary>
    /// A unique identifier.
    /// </summary>
    [Key]
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
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Whether the email has been verified or not.
    /// </summary>
    public bool IsEmailVerified { get; set; }

    /// <summary>
    /// A unique identifier of the photo representing the account's user.
    /// </summary>
    public int? PhotoId { get; set; }

    /// <summary>
    /// A unique identifier of the account that is responsible for creation of this account.
    /// </summary>
    /// <remarks>
    /// Equal to <see cref="Id"/> if the account has been created by the user it represents.
    /// </remarks>
    public int CreatedById { get; set; }

    /// <summary>
    /// A navigation corresponding to <see cref="CreatedById"/>.
    /// </summary>
    [ForeignKey(nameof(CreatedById))]
    public Account CreatedBy { get; set; } = null!;

    /// <summary>
    /// A point in time denoting when the account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// A unique identifier of the account that is responsible for being the last one to update this account's information.
    /// </summary>
    /// <remarks>
    /// Equal to <see cref="Id"/> if the account has been updated by the user it represents.
    /// <para/>
    /// Is null if the account has not been updated yet.
    /// </remarks>
    public int? UpdatedById { get; set; }

    /// <summary>
    /// A navigation corresponding to <see cref="UpdatedById"/>.
    /// </summary>
    /// <remarks>
    /// Is null if <see cref="UpdatedById"/> is null.
    /// </remarks>
    [ForeignKey(nameof(UpdatedById))]
    public Account? UpdatedBy { get; set; }

    /// <summary>
    /// A point in time denoting when the account was last updated.
    /// </summary>
    /// <remarks>
    /// Is null if <see cref="UpdatedById"/> is null.
    /// </remarks>
    public DateTime? UpdatedAt { get; set; }
}