namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;

public class CreatePatientRequest
{
    public required Guid AccountId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required string PhoneNumber { get; init; }
    public required DateOnly DateOfBirth { get; init; }
}