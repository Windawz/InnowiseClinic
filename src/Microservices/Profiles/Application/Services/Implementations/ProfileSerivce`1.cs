using InnowiseClinic.Microservices.Profiles.Application.Exceptions;
using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories;
using InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Implementations;

public abstract class ProfileService<TProfile> : IProfileSerivce<TProfile> where TProfile : Profile
{
    protected ProfileService(IProfileRepository repository)
    {
        Repository = repository;
    }

    protected IProfileRepository Repository { get; }

    public async Task CreateAsync(TProfile newProfile)
    {
        var name = newProfile.Name;

        if (await Repository.GetAsync<TProfile>(new NameFilter(name)) is not null)
        {
            throw new ProfileWithGivenNameAlreadyExistsException(name);
        }

        await Repository.AddAsync(newProfile);
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await Repository.DeleteAsync(id);

        if (result is RepositoryDeleteResult.DoesNotExist)
        {
            throw new ProfileNotFoundByIdException(id);
        }
    }

    public async Task EditAsync(Guid id, Action<TProfile> editAction)
    {
        var profile = await Repository.GetAsync<TProfile>(id);

        if (profile is null)
        {
            throw new ProfileNotFoundByIdException(id);
        }

        editAction(profile);

        var result = await Repository.UpdateAsync(profile);

        if (result is RepositoryUpdateResult.DoesNotExist)
        {
            throw new ProfileNotFoundByIdException(id);
        }
    }

    public async Task<TProfile> GetAsync(Guid id)
    {
        return await Repository.GetAsync<TProfile>(id)
            ?? throw new ProfileNotFoundByIdException(id);
    }

    public async Task<TProfile?> GetByNameAsync(Name name)
    {
        return await Repository.GetAsync<TProfile>(new NameFilter(name));
    }

    public async Task<ICollection<TProfile>> GetManyAsync(int? lastPosition, int? maxCount)
    {
        return await Repository.GetManyAsync<TProfile>(
            filter: null,
            lastPosition: lastPosition,
            maxCount: maxCount);
    }
}