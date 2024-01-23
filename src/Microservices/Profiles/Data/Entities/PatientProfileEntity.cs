namespace InnowiseClinic.Microservices.Profiles.Data.Entities;

public record PatientProfileEntity(
    Guid Id,
    Guid AccountId,
    string FirstName,
    string LastName,
    string? MiddleName,
    string PhoneNumber,
    DateOnly DateOfBirth)
        : ProfileEntity(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);