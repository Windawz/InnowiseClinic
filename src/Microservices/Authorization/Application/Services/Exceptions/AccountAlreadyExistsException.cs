namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class AccountAlreadyExistsException(string email) : Exception
{
    public string Email { get; } = email;

    public override string Message =>
        $"Account with email \"{Email}\" already exists";
}