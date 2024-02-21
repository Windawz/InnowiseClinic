using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

public interface IDoctorProfileService : IProfileSerivce<DoctorProfile>
{
    Task<ICollection<DoctorProfile>> GetManyByOfficeAsync(Guid officeId, int? lastPosition, int? maxCount);
    Task<ICollection<DoctorProfile>> GetManyBySpecializationAsync(Guid specializationId, int? lastPosition, int? maxCount);
    Task ChangeStatusAsync(Guid id, DoctorStatus status);
}