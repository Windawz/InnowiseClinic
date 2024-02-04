namespace InnowiseClinic.Microservices.Offices.Application.Models;

public record Office(
    Guid Id,
    string City,
    string Street,
    string HouseNumber,
    string? OfficeNumber,
    string RegistryPhoneNumber,
    bool IsActive);