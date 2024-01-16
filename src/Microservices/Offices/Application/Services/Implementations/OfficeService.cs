using InnowiseClinic.Microservices.Offices.Application.Exceptions;
using InnowiseClinic.Microservices.Offices.Application.Mapping;
using InnowiseClinic.Microservices.Offices.Application.Models;
using InnowiseClinic.Microservices.Offices.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Offices.Data.Entities;
using InnowiseClinic.Microservices.Offices.Data.Repositories.Interfaces;

namespace InnowiseClinic.Microservices.Offices.Application.Services.Implementations;

public class OfficeService : IOfficeService
{
    private readonly IOfficeRepository _officeRepository;

    public OfficeService(
        IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    /// <exception cref="OfficeNotFoundException"/>
    public async Task<Office> GetOfficeAsync(Guid id)
    {
        OfficeEntity? officeEntity = await _officeRepository.GetAsync(id);
        
        if (officeEntity is null)
        {
            throw new OfficeNotFoundException(id);
        }

        return OfficeMapping.ToOffice(officeEntity);
    }

    /// <exception cref="OfficeNotFoundException"/>
    public async Task<ICollection<Office>> GetOfficePageAsync(int count, Guid? start = null)
    {
        if (start is Guid guid && await _officeRepository.GetAsync(guid) is null)
        {
            throw new OfficeNotFoundException(guid);
        }

        var officeEntities = await _officeRepository.GetPageAsync(count, start);

        return officeEntities.Select(entity => OfficeMapping.ToOffice(entity))
            .ToList();
    }

    public async Task<Guid> CreateOfficeAsync(OfficeCreationInput input)
    {
        var officeEntity = new OfficeEntity()
        {
            Id = default,
            City = input.City,
            Street = input.Street,
            HouseNumber = input.HouseNumber,
            OfficeNumber = input.OfficeNumber,
            RegistryPhoneNumber = input.RegistryPhoneNumber,
            IsActive = input.IsActive,
        };

        await _officeRepository.AddAsync(officeEntity);
        
        return officeEntity.Id;
    }

    /// <exception cref="OfficeNotFoundException"/>
    public async Task EditOfficeAsync(Guid id, OfficeEditInput input)
    {
        var office = OfficeMapping.ToOffice(
            await _officeRepository.GetAsync(id)
                ?? throw new OfficeNotFoundException(id));

        var newOffice = office with
        {
            City = input.City,
            Street = input.Street,
            HouseNumber = input.HouseNumber,
            OfficeNumber = input.OfficeNumber,
            RegistryPhoneNumber = input.RegistryPhoneNumber,
            IsActive = input.IsActive,
        };

        await _officeRepository.UpdateAsync(
            OfficeMapping.ToOfficeEntity(newOffice));
    }
}