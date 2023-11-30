using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services.Services;

public class AccountAlreadyExistsException : BusinessServiceException
{
    public AccountAlreadyExistsException(Email email) : base($"Account with email \"{email}\" already exists")
    {
        Email = email;
    }

    public Email Email { get; }
}