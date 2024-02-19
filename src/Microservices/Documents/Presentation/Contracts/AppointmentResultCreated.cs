namespace InnowiseClinic.Microservices.Documents.Presentation.Contracts;

public class AppointmentResultCreated
{
    public required Guid AppointmentId { get; set; }
    public required string? Extension { get; set; }
    public required byte[] AppointmentResultData { get; set; }
}