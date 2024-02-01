using InnowiseClinic.Microservices.Profiles.Application.Exceptions;
using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Implementations;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;

    public DoctorService(IDoctorRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<DoctorProfile> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetAsync(id)
            ?? throw new ProfileNotFoundByIdException(id);
        var model = ApplicationToDataMap.ToModel(entity);

        return model;
    }

    public async Task<DoctorProfile> GetByNameAsync(Name name)
    {
        var (first, last, middle) = name;
        var entity = await _repository.GetByNamePartsAsync(first, last, middle)
            ?? throw new ProfileNotFoundByNameException(name);
        var model = ApplicationToDataMap.ToModel(entity);

        return model;
    }

    public async Task<ICollection<DoctorProfile>> GetPageAsync(int offset, int count)
    {
        var entities = await _repository.GetPageAsync(offset, count);

        return entities
            .Select(ApplicationToDataMap.ToModel)
            .ToList();
    }

    public async Task<DoctorProfile> CreateAsync(DoctorProfile input)
    {
        var entity = ApplicationToDataMap.ToEntity(input);
        var id = await _repository.AddAsync(entity);

        return input with { Id = id };
    }

    public async Task EditAsync(Guid id, DoctorProfile input)
    {
        if (await _repository.GetAsync(id) is null)
        {
            throw new ProfileNotFoundByIdException(id);
        }

        var entity = ApplicationToDataMap.ToEntity(input) with
        {
            Id = id,
        };
        
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetAsync(id);

        if (entity is null)
        {
            throw new ProfileNotFoundByIdException(id);
        }

        await _repository.DeleteAsync(entity);
    }

    public async Task<ICollection<DoctorProfile>> GetPageByOfficeAsync(int offset, int count, Guid officeId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var entities = await _repository.GetPageFilteredByOfficeAsync(offset, count, officeId);

        return entities.Select(ApplicationToDataMap.ToModel)
            .ToList();
    }

    public async Task<ICollection<DoctorProfile>> GetPageBySpecializationAsync(int offset, int count, Guid specializationId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var entities = await _repository.GetPageFilteredBySpecializationAsync(offset, count, specializationId);

        return entities.Select(ApplicationToDataMap.ToModel)
            .ToList();
    }
}