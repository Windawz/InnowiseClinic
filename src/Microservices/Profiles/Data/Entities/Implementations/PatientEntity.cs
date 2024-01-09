namespace InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;

public record PatientEntity(
    Guid Id,
    Guid AccountId,
    string FirstName,
    string LastName,
    string? MiddleName,
    string PhoneNumber,
    DateOnly DateOfBirth) : Entity(Id);