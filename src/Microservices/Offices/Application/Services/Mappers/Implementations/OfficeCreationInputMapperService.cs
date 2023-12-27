using InnowiseClinic.Microservices.Offices.Application.Models;
using InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Offices.Data.Entities;

namespace InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Implementations;

public class OfficeCreationInputMapperService : IOfficeCreationInputMapperService
{
    public OfficeEntity MapToOfficeEntity(OfficeCreationInput input)
    {
        return new()
        {
            City = input.City,
            Street = input.Street,
            HouseNumber = input.HouseNumber,
            OfficeNumber = input.OfficeNumber,
            PhotoId = input.PhotoId,
            RegistryPhoneNumber = input.RegistryPhoneNumber,
            IsActive = input.IsActive,
        };
    }
}