using System.Collections.Immutable;

namespace InnowiseClinic.Services.Authorization.Services.Models;
/// <summary>
/// Represents a user account.
/// </summary>
/// <param name="Id">A unique identifier.</param>
/// <param name="Email">An email address.</param>
/// <param name="Password">A literal password string.</param>
/// <param name="PhoneNumber">A phone number.</param>
/// <param name="IsEmailVerified">Whether the email has been verified or not.</param>
/// <param name="PhotoId">A unique identifier of the photo representing the account's user.</param>
/// <param name="CreatedBy">The account that is responsible for creation of this account.</param>
/// <param name="CreatedAt">A point in time denoting when the account was created.</param>
/// <param name="UpdatedBy">The account that is responsible for being the last one to update this account's information.</param>
/// <param name="UpdatedAt">A point in time denoting when the account was last updated.</param>
/// <param name="Roles">The roles assigned to the account.</param>
public record Account(
    int Id,
    string Email,
    string Password,
    string PhoneNumber,
    bool IsEmailVerified,
    int? PhotoId,
    Account? CreatedBy,
    DateTime CreatedAt,
    Account? UpdatedBy,
    DateTime? UpdatedAt,
    IImmutableSet<Role> Roles);