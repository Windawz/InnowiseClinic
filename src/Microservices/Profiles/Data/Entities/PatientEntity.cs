namespace InnowiseClinic.Microservices.Profiles.Data.Entities;

public record PatientEntity(
    Guid Id,
    Guid AccountId,
    string FirstName,
    string LastName,
    string? MiddleName,
    string PhoneNumber,
    DateOnly DateOfBirth)
        : Entity(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);