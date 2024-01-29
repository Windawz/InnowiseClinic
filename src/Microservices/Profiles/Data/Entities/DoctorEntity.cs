namespace InnowiseClinic.Microservices.Profiles.Data.Entities;

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
    int Status)
        : Entity(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);