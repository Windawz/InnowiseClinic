using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services.Services;

public class AccountWithGivenEmailDoesNotExistException : FailedToLogInException
{
    public AccountWithGivenEmailDoesNotExistException(string emailAddress) : base($"Account with email {emailAddress} does not exist")
    {
        EmailAddress = emailAddress;
    }

    public string EmailAddress { get; }
}