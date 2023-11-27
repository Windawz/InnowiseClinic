using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// Registers new accounts.
/// </summary>
public interface IAccountRegistrator
{
    /// <summary>
    /// Registers an account on behalf of said account.
    /// </summary>
    /// <param name="email">Account email.</param>
    /// <param name="password">Account password in plain text.</param>
    /// <exception cref="AccountAlreadyExistsException">
    /// Thrown when an account is already registered with the provided email.
    /// </exception>
    /// <returns>
    /// The registered account.
    /// </returns>
    Account RegisterSelf(Email email, string password);
    /// <summary>
    /// Registers an account on behalf of <paramref name="initiator"/>.
    /// </summary>
    /// <param name="initiator">The account that is responsible for registering this account.</param>
    /// <param name="email">Account email.</param>
    /// <param name="password">Account password in plain text.</param>
    /// <param name="roles">Account roles.</param>
    /// <exception cref="AccountAlreadyExistsException">
    /// Thrown when an account is already registered with the provided email.
    /// </exception>
    /// <exception cref="NotPermittedToAssignRoleException">
    /// Thrown when the <paramref name="initiator"/> account tries to
    /// register an account and assign to it roles that it is not permitted to.
    /// </exception>
    /// <returns>
    /// The registered account.
    /// </returns>
    Account RegisterOther(Account initiator, Email email, string password, IReadOnlyCollection<Role> roles);
}