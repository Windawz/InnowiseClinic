using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

public interface IDoctorService : IProfileService<DoctorProfile>
{
    Task<ICollection<DoctorProfile>> GetPageByOfficeAsync(int offset, int count, Guid officeId);
    Task<ICollection<DoctorProfile>> GetPageBySpecializationAsync(int offset, int count, Guid specializationId);
}