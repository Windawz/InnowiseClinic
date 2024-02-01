namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public record PatientProfile(
    Guid Id,
    Guid AccountId,
    Name Name,
    string PhoneNumber,
    DateOnly DateOfBirth)
        : Profile(
            Id: Id,
            AccountId: AccountId,
            Name: Name);