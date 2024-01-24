namespace InnowiseClinic.Microservices.Profiles.Data.Entities;

public record ReceptionistEntity(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    string FirstName,
    string LastName,
    string? MiddleName)
        : Entity(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);