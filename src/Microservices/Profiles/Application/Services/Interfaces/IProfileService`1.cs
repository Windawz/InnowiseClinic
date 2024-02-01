using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

public interface IProfileService<TProfile> where TProfile : Profile
{
    Task<TProfile> GetByIdAsync(Guid id);
    Task<TProfile> GetByNameAsync(Name name);
    Task<ICollection<TProfile>> GetPageAsync(int offset, int count);
    Task<TProfile> CreateAsync(TProfile input);
    Task EditAsync(Guid id, TProfile input);
    Task DeleteAsync(Guid id);
}