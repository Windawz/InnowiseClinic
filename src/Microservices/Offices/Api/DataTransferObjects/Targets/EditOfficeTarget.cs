namespace InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Targets;

public class EditOfficeTarget
{
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string HouseNumber { get; set; }
    public string? OfficeNumber { get; set; }
    public required string RegistryPhoneNumber { get; set; }
    public required bool IsActive { get; set; }
}