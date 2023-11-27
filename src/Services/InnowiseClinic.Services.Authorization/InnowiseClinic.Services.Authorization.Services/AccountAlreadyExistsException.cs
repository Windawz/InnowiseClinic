using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// Thrown when an attempt is made to register an account with an email that is
/// already used by a different, already registered account.
/// </summary>
public class AccountAlreadyExistsException : BusinessLogicException
{
    /// <summary>
    /// Creates an instance of <see cref="AccountAlreadyExistsException"/>.
    /// </summary>
    /// <param name="email">The conflicting email of the account.</param>
    public AccountAlreadyExistsException(Email email) : base($"Account with email \"{email}\" already exists")
    {
        Email = email;
    }

    /// <summary>
    /// The conflicting email of the account.
    /// </summary>
    public Email Email { get; }
}