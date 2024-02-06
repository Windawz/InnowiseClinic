namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public class ReceptionistProfile : Profile
{
    public ReceptionistProfile(Guid accountId, Guid officeId, Name name) : base(accountId, name)
    {
        OfficeId = officeId;
    }

    public Guid OfficeId { get; set; }
}