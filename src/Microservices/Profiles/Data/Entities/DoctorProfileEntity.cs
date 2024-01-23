namespace InnowiseClinic.Microservices.Profiles.Data.Entities;

public record DoctorProfileEntity(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    Guid SpecializationId,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    int CareerStartYear,
    DoctorStatus Status)
        : ProfileEntity(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);