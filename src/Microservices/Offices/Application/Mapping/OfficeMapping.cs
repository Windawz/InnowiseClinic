using InnowiseClinic.Microservices.Offices.Application.Models;
using InnowiseClinic.Microservices.Offices.Data.Entities;

namespace InnowiseClinic.Microservices.Offices.Application.Mapping;

public static class OfficeMapping
{
    public static Office ToOffice(OfficeEntity entity)
    {
        return new(
            Id: entity.Id,
            City: entity.City,
            Street: entity.Street,
            HouseNumber: entity.HouseNumber,
            OfficeNumber: entity.OfficeNumber,
            RegistryPhoneNumber: entity.RegistryPhoneNumber,
            IsActive: entity.IsActive);
    }

    public static OfficeEntity ToOfficeEntity(Office office)
    {
        return new()
        {
            Id = office.Id,
            City = office.City,
            Street = office.Street,
            HouseNumber = office.HouseNumber,
            OfficeNumber = office.OfficeNumber,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }
}