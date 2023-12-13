namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class InvalidPasswordException(string email, string password) : Exception
{
    public string Email { get; } = email;

    public string Password { get; } = password;

    public override string Message =>
        $"Password provided to gain access to account with email \"{Email}\" is invalid";
}