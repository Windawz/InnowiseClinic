using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Offices.Application.Models;

namespace InnowiseClinic.Microservices.Offices.Api.Mapping;

public static class RequestMapping
{
    public static OfficeCreationInput ToOfficeCreationInput(CreateOfficeRequest request, Guid? photoId)
    {
        throw new NotImplementedException();
    }

    public static OfficeEditInput ToOfficeEditInput(EditOfficeRequest request, Guid? photoId)
    {
        throw new NotImplementedException();
    }
}