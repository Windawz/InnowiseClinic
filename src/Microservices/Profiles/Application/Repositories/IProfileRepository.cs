using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;

namespace InnowiseClinic.Microservices.Profiles.Application.Repositories;

public interface IProfileRepository
{
    Task<TProfile?> GetAsync<TProfile>(Guid id) where TProfile : Profile;
    Task<TProfile?> GetAsync<TProfile>(IFilter filter) where TProfile : Profile;
    Task<ICollection<TProfile>> GetManyAsync<TProfile>(
        IFilter? filter,
        int? lastPosition,
        int? maxCount) where TProfile : Profile;
    Task<RepositoryAddResult> AddAsync<TProfile>(TProfile profile) where TProfile : Profile;
    Task<RepositoryUpdateResult> UpdateAsync<TProfile>(TProfile profile) where TProfile : Profile;
    Task<RepositoryDeleteResult> DeleteAsync(Guid id);
}