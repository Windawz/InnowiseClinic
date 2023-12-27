using InnowiseClinic.Microservices.Offices.Application.Models;
using InnowiseClinic.Microservices.Offices.Data.Entities;

namespace InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Interfaces;

public interface IOfficeCreationInputMapperService
{
    OfficeEntity MapToOfficeEntity(OfficeCreationInput input);
}