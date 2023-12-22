namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException(Guid tokenId)
    {
        TokenId = tokenId;
    }

    public Guid TokenId { get; }

    public override string Message =>
        $"Refresh token with id \"{TokenId}\" is not valid";
}