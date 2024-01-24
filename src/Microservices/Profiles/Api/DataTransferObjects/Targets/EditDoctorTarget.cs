namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;

public class EditDoctorTarget
{
    public required Guid OfficeId { get; set; }
    public required Guid SpecializationId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required int CareerStartYear { get; set; }
    public required int Status { get; set; }
}