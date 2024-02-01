namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public record ReceptionistProfile(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    Name Name)
        : Profile(
            Id: Id,
            AccountId: AccountId,
            Name: Name);