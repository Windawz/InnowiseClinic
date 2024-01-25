namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public record ReceptionistProfile(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    string FirstName,
    string LastName,
    string? MiddleName)
        : Profile(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);