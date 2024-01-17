namespace InnowiseClinic.Microservices.Offices.Data.Views;

public class OfficePageEntryView
{
    public required Guid OfficeId { get; set; }
    public string? OfficeNumber { get; set; }
    public required string RegistryPhoneNumber { get; set; }
    public required bool IsActive { get; set; }
}