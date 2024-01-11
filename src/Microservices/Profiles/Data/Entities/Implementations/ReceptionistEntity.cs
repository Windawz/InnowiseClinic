namespace InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;

public record ReceptionistEntity(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    string FirstName,
    string LastName,
    string? MiddleName) : Entity(Id);