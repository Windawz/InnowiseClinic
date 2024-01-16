using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Offices.Application.Models;

namespace InnowiseClinic.Microservices.Offices.Api.Mapping;

public static class TargetMapping
{
    public static EditOfficeTarget FromOffice(Office office)
    {
        return new()
        {
            City = office.City,
            Street = office.Street,
            HouseNumber = office.HouseNumber,
            OfficeNumber = office.OfficeNumber,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive,
        };
    }

    public static OfficeEditInput ToOfficeEditInput(EditOfficeTarget target)
    {
        return new(
            City: target.City.Trim(),
            Street: target.Street.Trim(),
            HouseNumber: target.HouseNumber.Trim(),
            OfficeNumber: target.OfficeNumber?.Trim(),
            RegistryPhoneNumber: target.RegistryPhoneNumber.Trim(),
            IsActive: target.IsActive);
    }
}