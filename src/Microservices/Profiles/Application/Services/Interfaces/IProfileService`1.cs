using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories.Filtering;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

public interface IProfileSerivce<TProfile> where TProfile : Profile
{
    Task<TProfile> GetAsync(Guid id);
    Task<ICollection<TProfile>> GetManyAsync(int? lastPosition, int? maxCount);
    Task<ICollection<TProfile>> GetManyByNameAsync(FilteredName filteredName, int? lastPosition, int? maxCount);
    Task CreateAsync(TProfile newProfile);
    Task EditAsync(Guid id, Action<TProfile> editAction);
    Task DeleteAsync(Guid id);
}