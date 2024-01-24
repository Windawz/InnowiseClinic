namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;

public class CreateDoctorRequest
{
    public required Guid OfficeId { get; init; }
    public required Guid SpecializationId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required DateOnly DateOfBirth { get; init; }
    public required int CareerStartYear { get; init; }
    public required int Status { get; init; }
}