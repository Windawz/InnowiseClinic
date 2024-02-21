namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;

public class EditReceptionistTarget
{
    public required Guid OfficeId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
}