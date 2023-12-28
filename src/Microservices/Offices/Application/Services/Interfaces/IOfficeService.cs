using InnowiseClinic.Microservices.Offices.Application.Exceptions;
using InnowiseClinic.Microservices.Offices.Application.Models;

namespace InnowiseClinic.Microservices.Offices.Application.Services.Interfaces;

public interface IOfficeService
{
    /// <exception cref="OfficeNotFoundException"/>
    Task<Office> GetOfficeAsync(Guid id);
    /// <exception cref="OfficeNotFoundException"/>
    Task<ICollection<Office>> GetOfficePageAsync(int count, Guid? start = null);
    Task<Guid> CreateOfficeAsync(OfficeCreationInput input);
    /// <exception cref="OfficeNotFoundException"/>
    Task EditOfficeAsync(Guid id, OfficeEditInput input);
}