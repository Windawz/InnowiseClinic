namespace InnowiseClinic.Microservices.Profiles.Data.Documents;

public record ProfileDocument(
    Guid Id,
    Guid AccountId,
    Guid? OfficeId,
    Guid? SpecializationId,
    ProfileType Type,
    string FirstName,
    string LastName,
    string? MiddleName,
    string? PhoneNumber,
    DateOnly? DateOfBirth,
    int? CareerStartYear,
    int? DoctorStatus)
{
    public static readonly string CollectionName = "Profiles";
}