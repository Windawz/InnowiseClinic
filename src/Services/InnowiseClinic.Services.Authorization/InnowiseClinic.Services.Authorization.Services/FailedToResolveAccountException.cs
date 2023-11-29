namespace InnowiseClinic.Services.Authorization.Services;

public class FailedToResolveAccountException : InfrastructureException
{
    public FailedToResolveAccountException(int accountId) : base("Failed to resolve account with given id")
    {
        AccountId = accountId;
    }

    public int AccountId { get; }
}