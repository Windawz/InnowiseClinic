namespace InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;

public record DoctorEntity(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    Guid SpecializationId,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    int CareerStartYear,
    int Status) : Entity(Id);