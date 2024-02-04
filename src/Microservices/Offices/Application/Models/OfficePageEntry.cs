namespace InnowiseClinic.Microservices.Offices.Application.Models;

public record OfficePageEntry(
    Guid OfficeId,
    string? OfficeNumber,
    string RegistryPhoneNumber,
    bool IsActive);