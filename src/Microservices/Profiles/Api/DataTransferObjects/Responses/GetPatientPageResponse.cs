namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;

public class GetPatientPageResponse
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required string PhoneNumber { get; init; }
}