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
    /// <returns>
    /// The registered account.
    /// </returns>
    Account RegisterSelf(string email, string password);
    /// <summary>
    /// Registers an account on behalf of <paramref name="initiator"/>.
    /// </summary>
    /// <param name="initiator">The account that is responsible for registering this account.</param>
    /// <param name="email">Account email.</param>
    /// <param name="password">Account password in plain text.</param>
    /// <param name="roles">Account roles.</param>
    /// <returns>
    /// The registered account.
    /// </returns>
    Account RegisterOther(Account initiator, string email, string password, IReadOnlyCollection<Role> roles);
}