namespace InnowiseClinic.Microservices.Authorization.Application.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string email)
    {
        Email = email;
    }

    public string Email { get; }

    public override string Message =>
        $"Failed to find account with email \"{Email}\"";
}