namespace InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

public class RegisterRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string ConfirmationPassword { get; init; }
}