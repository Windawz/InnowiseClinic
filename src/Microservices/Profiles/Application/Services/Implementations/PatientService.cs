using InnowiseClinic.Microservices.Profiles.Application.Exceptions;
using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Implementations;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<PatientProfile> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetAsync(id)
            ?? throw new ProfileNotFoundByIdException(id);
        var model = ApplicationToDataMap.ToModel(entity);

        return model;
    }

    public async Task<PatientProfile> GetByNameAsync(Name name)
    {
        var (first, last, middle) = name;
        var entity = await _repository.GetByNamePartsAsync(first, last, middle)
            ?? throw new ProfileNotFoundByNameException(name);
        var model = ApplicationToDataMap.ToModel(entity);

        return model;
    }

    public async Task<ICollection<PatientProfile>> GetPageAsync(int offset, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var entities = await _repository.GetPageAsync(offset, count);

        return entities
            .Select(ApplicationToDataMap.ToModel)
            .ToList();
    }

    public async Task<PatientProfile> CreateAsync(PatientProfile input)
    {
        var entity = ApplicationToDataMap.ToEntity(input);
        var id = await _repository.AddAsync(entity);

        return input with { Id = id };
    }

    public async Task EditAsync(Guid id, PatientProfile input)
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
}