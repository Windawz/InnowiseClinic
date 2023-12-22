namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class AccountAlreadyExistsException : Exception
{
    public AccountAlreadyExistsException(string email)
    {
        Email = email;
    }

    public string Email { get; }

    public override string Message =>
        $"Account with email \"{Email}\" already exists";
}