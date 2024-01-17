using InnowiseClinic.Microservices.Offices.Application.Models;
using InnowiseClinic.Microservices.Offices.Data.Views;

namespace InnowiseClinic.Microservices.Offices.Application.Mapping;

public static class OfficePageEntryMapping
{
    public static OfficePageEntry FromView(OfficePageEntryView view)
    {
        return new(
            OfficeId: view.OfficeId, 
            OfficeNumber: view.OfficeNumber, 
            RegistryPhoneNumber: view.RegistryPhoneNumber, 
            IsActive: view.IsActive);
    }
}