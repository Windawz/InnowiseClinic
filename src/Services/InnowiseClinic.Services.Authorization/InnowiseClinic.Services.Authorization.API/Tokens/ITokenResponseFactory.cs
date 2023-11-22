namespace InnowiseClinic.Services.Authorization.API.Tokens;

/// <summary>
/// Produces instances of <see cref="TokenResponse"/>.
/// </summary>
internal interface ITokenResponseFactory
{
    /// <summary>
    /// Creates a <see cref="TokenResponse"/> from given user info.
    /// </summary>
    /// <param name="accountId">
    /// User account ID to be used in generation of the access token.
    /// </param>
    /// <param name="accountRoleNames">
    /// Names of the roles that the user belongs to to be used in generation of the access token.
    /// </param>
    /// <returns>An instance of <see cref="TokenResponse"/>.</returns>
    TokenResponse Create(int accountId, IReadOnlyCollection<string> accountRoleNames);
}