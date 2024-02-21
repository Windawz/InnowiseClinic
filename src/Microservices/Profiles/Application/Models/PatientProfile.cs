namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public class PatientProfile : Profile
{
    private DateOnly _dateOfBirth;

    public PatientProfile(
        Guid accountId,
        Name name,
        string phoneNumber,
        DateOnly dateOfBirth) : base(accountId, name)
    {
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }
    
    public string PhoneNumber { get; set; }

    public DateOnly DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            Dates.ThrowIfDateIsOutOfRange(value);

            _dateOfBirth = value;
        }
    }
}