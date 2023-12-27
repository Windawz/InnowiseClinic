using InnowiseClinic.Microservices.Offices.Application.Models;
using InnowiseClinic.Microservices.Offices.Data.Entities;

namespace InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Interfaces;

public interface IOfficeMapperService
{
    Office MapFromOfficeEntity(OfficeEntity entity);
    OfficeEntity MapToOfficeEntity(Office office);
}