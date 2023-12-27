using InnowiseClinic.Microservices.Offices.Application.Models;
using InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Offices.Data.Entities;

namespace InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Implementations;

public class OfficeMapperService : IOfficeMapperService
{
    public Office MapFromOfficeEntity(OfficeEntity entity)
    {
        return new(
            Id: entity.Id,
            City: entity.City,
            Street: entity.Street,
            HouseNumber: entity.HouseNumber,
            OfficeNumber: entity.OfficeNumber,
            PhotoId: entity.PhotoId,
            RegistryPhoneNumber: entity.RegistryPhoneNumber,
            IsActive: entity.IsActive);
    }

    public OfficeEntity MapToOfficeEntity(Office office)
    {
        return new()
        {
            Id = default,
            City = office.City,
            Street = office.Street,
            HouseNumber = office.HouseNumber,
            OfficeNumber = office.OfficeNumber,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }
}
