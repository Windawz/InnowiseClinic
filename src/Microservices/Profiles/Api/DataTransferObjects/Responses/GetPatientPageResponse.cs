namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;

public class GetPatientPageResponse
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
}