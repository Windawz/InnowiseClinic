namespace InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;

public abstract class TokenResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required string TokenType { get; init; }
    public required int ExpiresInSeconds { get; init; }
}