using InnowiseClinic.Microservices.Profiles.Application.Exceptions;

namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public class DoctorProfile : Profile
{
    private DateOnly _dateOfBirth;
    private int _careerStartYear;
    private DoctorStatus _status;

    public DoctorProfile(
        Guid accountId,
        Guid officeId,
        Guid specializationId,
        Name name,
        DateOnly dateOfBirth,
        int careerStartYear,
        DoctorStatus status) : base(accountId, name)
    {
        OfficeId = officeId;
        SpecializationId = specializationId;
        DateOfBirth = dateOfBirth;
        CareerStartYear = careerStartYear;
    }

    public Guid OfficeId { get; set; }

    public Guid SpecializationId { get; set; }

    public DateOnly DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            Dates.ThrowIfDateIsOutOfRange(value);
            ThrowIfDateOfBirthExceedsCareerStartYear(value, CareerStartYear);

            _dateOfBirth = value;
        }
    }

    public int CareerStartYear
    {
        get => _careerStartYear;
        set
        {
            Dates.ThrowIfYearIsOutOfRange(value);
            ThrowIfDateOfBirthExceedsCareerStartYear(DateOfBirth, value);

            _careerStartYear = value;
        }
    }

    public DoctorStatus Status
    {
        get => _status;
        set
        {
            if (!Enum.IsDefined(value))
            {
                // Not a domain error.
                // The API layer will validate the status on its own.
                // Therefore, this is only thrown if an invalid value
                // is assigned programmatically.
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(value),
                    message: $"Invalid doctor status value {value}");
            }

            _status = value;
        }
    }

    public int ExperienceYearCount =>
        Math.Max(0, DateTime.UtcNow.Year - CareerStartYear) + 1;

    private static void ThrowIfDateOfBirthExceedsCareerStartYear(DateOnly dateOfBirth, int careerStartYear)
    {
        if (dateOfBirth.Year >= careerStartYear)
        {
            throw new DateOfBirthExceedsCareerStartYearException(dateOfBirth, careerStartYear);
        }
    }
}