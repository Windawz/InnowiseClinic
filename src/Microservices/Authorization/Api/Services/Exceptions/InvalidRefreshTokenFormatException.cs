namespace InnowiseClinic.Microservices.Authorization.Api.Services.Exceptions;

public class InvalidRefreshTokenFormatException(string refreshTokenString) : Exception
{
    public string RefreshTokenString { get; } = refreshTokenString;

    public override string Message =>
        $"Cannot parse refresh token from string \"{RefreshTokenString}\"";
}