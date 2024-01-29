namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public record DoctorProfile(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    Guid SpecializationId,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    int CareerStartYear,
    DoctorStatus Status)
        : Profile(
            Id: Id,
            AccountId: AccountId,
            FirstName: FirstName,
            LastName: LastName,
            MiddleName: MiddleName)
{
    public int Experience =>
        Math.Max(0,
            DateOnly.FromDateTime(DateTime.UtcNow)
                .Year - CareerStartYear) + 1;
}