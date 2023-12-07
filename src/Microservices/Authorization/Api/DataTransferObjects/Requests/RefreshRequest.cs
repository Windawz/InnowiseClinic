namespace InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

public class RefreshRequest
{
    public required string RefreshToken { get; init; }
}