namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }

    public string Password { get; }

    public override string Message =>
        $"Password provided to gain access to account with email \"{Email}\" is invalid";
}