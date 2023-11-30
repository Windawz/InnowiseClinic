namespace InnowiseClinic.Services.Authorization.Services.Services;

public class FailedToResolveAccountException : InternalException
{
    public FailedToResolveAccountException(int accountId) : base("Failed to resolve account with given id")
    {
        AccountId = accountId;
    }

    public int AccountId { get; }
}