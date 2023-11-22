using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// Searches for accounts by various criteria.
/// </summary>
public interface IAccountResolver
{
    /// <summary>
    /// Searches for an account with a specific unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the account.</param>
    /// <returns>
    /// The account if found, null otherwise.
    /// </returns>
    Account? ResolveById(int id);
    /// <summary>
    /// Searches for an account with a specific email.
    /// </summary>
    /// <param name="email">The email of the account.</param>
    /// <returns>
    /// The account if found, null otherwise.
    /// </returns>
    Account? ResolveByEmail(string email);
}