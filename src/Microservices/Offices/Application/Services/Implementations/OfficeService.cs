using InnowiseClinic.Microservices.Offices.Application.Models;
using InnowiseClinic.Microservices.Offices.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Offices.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Offices.Data.Entities;
using InnowiseClinic.Microservices.Offices.Data.Repositories.Interfaces;

namespace InnowiseClinic.Microservices.Offices.Application.Services.Implementations;

public class OfficeService : IOfficeService
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IOfficeMapperService _officeMapperService;
    private readonly IOfficeCreationInputMapperService _officeCreationInputMapperService;

    public OfficeService(
        IOfficeRepository officeRepository,
        IOfficeMapperService officeMapperService,
        IOfficeCreationInputMapperService officeCreationInputMapperService)
    {
        _officeRepository = officeRepository;
        _officeMapperService = officeMapperService;
        _officeCreationInputMapperService = officeCreationInputMapperService;
    }

    /// <exception cref="OfficeNotFoundException"/>
    public async Task<Office> GetOfficeAsync(Guid id)
    {
        OfficeEntity? officeEntity = await _officeRepository.GetAsync(id);
        
        if (officeEntity is not null)
        {
            return _officeMapperService.MapFromOfficeEntity(officeEntity);
        }
        else
        {
            throw new OfficeNotFoundException(id);
        }
    }

    public async Task<ICollection<Office>> GetOfficePageAsync(int count, Guid? start = null)
    {
        var officeEntities = await _officeRepository.GetPageAsync(count, start);
        // Ugly ToList() call.
        return officeEntities.Select(entity => _officeMapperService.MapFromOfficeEntity(entity))
            .ToList();
    }

    public async Task CreateOfficeAsync(OfficeCreationInput input)
    {
        var officeEntity = _officeCreationInputMapperService.MapToOfficeEntity(input);
        await _officeRepository.AddAsync(officeEntity);
    }

    /// <exception cref="OfficeNotFoundException"/>
    public async Task EditOfficeAsync(Guid id, OfficeEditInput input)
    {
         // Didn't want to use reflection here due to how
        // slow it is.

        OfficeEntity? officeEntity = await _officeRepository.GetAsync(id);
        
        if (officeEntity is null)
        {
            throw new OfficeNotFoundException(id);
        }

        var office = _officeMapperService.MapFromOfficeEntity(officeEntity);

        var newOffice = office with
        {
            City = input.City ?? office.City,
            Street = input.Street ?? office.Street,
            HouseNumber = input.HouseNumber ?? office.HouseNumber,
            OfficeNumber = input.OfficeNumber ?? office.OfficeNumber,
            PhotoId = input.PhotoId ?? office.PhotoId,
            RegistryPhoneNumber = input.RegistryPhoneNumber ?? office.RegistryPhoneNumber,
            IsActive = input.IsActive ?? office.IsActive,
        };

        var newOfficeEntity = _officeMapperService.MapToOfficeEntity(newOffice);

        await _officeRepository.UpdateAsync(newOfficeEntity);
    }
}