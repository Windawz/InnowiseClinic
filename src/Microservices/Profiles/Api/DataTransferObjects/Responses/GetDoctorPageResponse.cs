namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;

public class GetDoctorPageResponse
{
    public required Guid Id { get; init; }
    public required Guid OfficeId { get; init; }
    public required Guid SpecializationId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required int Experience { get; init; }
}