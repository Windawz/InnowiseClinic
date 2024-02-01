using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

public interface IReceptionistService : IProfileService<ReceptionistProfile>
{
    Task<ICollection<ReceptionistProfile>> GetPageByOfficeAsync(int offset, int count, Guid officeId);
}