using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.API.Tokens;

/// <summary>
/// Produces instances of <see cref="TokenResponse"/>.
/// </summary>
internal interface ITokenResponseFactory
{
    /// <summary>
    /// Creates a <see cref="TokenResponse"/> for given user account.
    /// </summary>
    /// <param name="account">
    /// The user account to generate tokens for.
    /// </param>
    /// <returns>An instance of <see cref="TokenResponse"/>.</returns>
    TokenResponse Create(Account account);
}