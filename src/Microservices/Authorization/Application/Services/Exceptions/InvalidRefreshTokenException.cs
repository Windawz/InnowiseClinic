namespace InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

public class InvalidRefreshTokenException(Guid tokenId) : Exception
{
    public Guid TokenId { get; } = tokenId;

    public override string Message =>
        $"Refresh token with id \"{TokenId}\" is not valid";
}