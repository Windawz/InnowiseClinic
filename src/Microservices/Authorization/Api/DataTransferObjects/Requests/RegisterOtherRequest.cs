namespace InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

public class RegisterOtherRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string ConfirmationPassword { get; init; }
    public required string Role { get; init; }
}