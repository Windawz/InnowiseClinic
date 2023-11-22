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
    /// <param name="info">An instance of <see cref="RegistrationInfo"/></param>
    /// <returns>
    /// The registered account.
    /// </returns>
    Account RegisterSelf(RegistrationInfo info);
    /// <summary>
    /// Registers an account on behalf of <paramref name="initiator"/>.
    /// </summary>
    /// <param name="info">An instance of <see cref="RegistrationInfo"/></param>
    /// <param name="initiator">The account that is responsible for registering this account.</param>
    /// <returns>
    /// The registered account.
    /// </returns>
    Account RegisterOther(RegistrationInfo info, Account initiator);
}