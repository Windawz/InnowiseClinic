namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public record DoctorProfile(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    Guid SpecializationId,
    string FirstName,
    string LastName,
    string? MiddleName) :
        Profile(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName);