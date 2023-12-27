using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Offices.Application.Models;

namespace InnowiseClinic.Microservices.Offices.Api.Services.Mappers.Interfaces;

public interface IGetOfficeResponseMapperService
{
    GetOfficeResponse MapFromOffice(Office office);
}