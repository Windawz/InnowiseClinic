using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Offices.Application.Models;

namespace InnowiseClinic.Microservices.Offices.Api.Mapping;

public static class RequestMapping
{
    public static OfficeCreationInput ToOfficeCreationInput(CreateOfficeRequest request)
    {
        return new(
            City: request.City.Trim(),
            Street: request.Street.Trim(),
            HouseNumber: request.HouseNumber.Trim(),
            OfficeNumber: request.OfficeNumber?.Trim(),
            RegistryPhoneNumber: request.RegistryPhoneNumber.Trim(),
            IsActive: request.IsActive);
    }

    public static OfficeEditInput ToOfficeEditInput(EditOfficeRequest request)
    {
        return new(
            City: request.City?.Trim(),
            Street: request.Street?.Trim(),
            HouseNumber: request.HouseNumber?.Trim(),
            OfficeNumber: request.OfficeNumber?.Trim(),
            RegistryPhoneNumber: request.RegistryPhoneNumber?.Trim(),
            IsActive: request.IsActive);
    }
}