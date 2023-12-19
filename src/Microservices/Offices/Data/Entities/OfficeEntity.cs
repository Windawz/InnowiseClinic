using InnowiseClinic.Microservices.Shared.Data.Entities;

namespace InnowiseClinic.Microservices.Offices.Data.Entities;

public class OfficeEntity : Entity
{
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string HouseNumber { get; set; }
    public string? OfficeNumber { get; set; }
    public Guid? PhotoId { get; set; }
    public required string RegistryPhoneNumber { get; set; }
    public required bool IsActive { get; set; }
}