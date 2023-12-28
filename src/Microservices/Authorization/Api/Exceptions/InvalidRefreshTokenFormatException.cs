namespace InnowiseClinic.Microservices.Authorization.Api.Exceptions;

public class InvalidRefreshTokenFormatException : Exception
{
    public InvalidRefreshTokenFormatException(string refreshTokenString)
    {
        RefreshTokenString = refreshTokenString;
    }

    public string RefreshTokenString { get; }

    public override string Message =>
        $"Cannot parse refresh token from string \"{RefreshTokenString}\"";
}