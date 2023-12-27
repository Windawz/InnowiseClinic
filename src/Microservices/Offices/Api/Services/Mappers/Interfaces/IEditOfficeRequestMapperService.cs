using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Offices.Application.Models;

namespace InnowiseClinic.Microservices.Offices.Api.Services.Mappers.Interfaces;

public interface IEditOfficeRequestMapperService
{
    OfficeEditInput MapToOfficeEditInput(EditOfficeRequest request, Guid? photoId);
}