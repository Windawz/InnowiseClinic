namespace InnowiseClinic.Microservices.Profiles.Data.Entities;

public record ReceptionistProfileEntity(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    string FirstName,
    string LastName,
    string? MiddleName)
        : ProfileEntity(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);