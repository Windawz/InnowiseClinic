namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class AccountNotFoundException(string email) : Exception
{
    public string Email { get; } = email;

    public override string Message =>
        $"Failed to find account with email \"{Email}\"";
}