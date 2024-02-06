using InnowiseClinic.Microservices.Profiles.Application.Exceptions;
using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories;
using InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Implementations;

public class DoctorProfileService : ProfileService<DoctorProfile>, IDoctorProfileService
{
    public DoctorProfileService(IProfileRepository repository) : base(repository) { }

    public async Task ChangeStatusAsync(Guid id, DoctorStatus status)
    {
        var profile = await Repository.GetAsync<DoctorProfile>(id)
            ?? throw new ProfileNotFoundByIdException(id);

        profile.Status = status;

        var result = await Repository.UpdateAsync(profile);

        if (result is RepositoryUpdateResult.DoesNotExist)
        {
            throw new ProfileNotFoundByIdException(id);
        }
    }

    public async Task<ICollection<DoctorProfile>> GetManyByOfficeAsync(Guid officeId, int? lastPosition, int? maxCount)
    {
        return await Repository.GetManyAsync<DoctorProfile>(
            filter: new OfficeFilter(officeId),
            lastPosition: lastPosition,
            maxCount: maxCount);
    }

    public async Task<ICollection<DoctorProfile>> GetManyBySpecialization(Guid specializationId, int? lastPosition, int? maxCount)
    {
        return await Repository.GetManyAsync<DoctorProfile>(
            filter: new SpecializationFilter(specializationId),
            lastPosition: lastPosition,
            maxCount: maxCount);
    }
}