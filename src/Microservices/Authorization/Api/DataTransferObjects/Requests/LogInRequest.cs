namespace InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

public class LogInRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}