namespace InnowiseClinic.Microservices.Offices.Application.Models;

public record OfficeEditInput(
    string? City,
    string? Street,
    string? HouseNumber,
    string? OfficeNumber,
    Guid? PhotoId,
    string? RegistryPhoneNumber,
    bool? IsActive);