namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public record PatientProfile(
    Guid Id,
    Guid AccountId,
    string FirstName,
    string LastName,
    string? MiddleName,
    string PhoneNumber,
    DateOnly DateOfBirth)
        : Profile(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);