namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public record DoctorProfile(
    Guid Id,
    Guid AccountId,
    Guid OfficeId,
    Guid SpecializationId,
    Name Name,
    DateOnly DateOfBirth,
    int CareerStartYear,
    DoctorStatus Status)
        : Profile(
            Id: Id,
            AccountId: AccountId,
            Name: Name)
{
    public int Experience =>
        Math.Max(0,
            DateOnly.FromDateTime(DateTime.UtcNow)
                .Year - CareerStartYear) + 1;
}