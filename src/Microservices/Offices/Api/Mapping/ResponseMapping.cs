using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Offices.Application.Models;

namespace InnowiseClinic.Microservices.Offices.Api.Mapping;

public static class ResponseMapping
{
    public static GetOfficeResponse ToGetOfficeResponse(Office office)
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