using InnowiseClinic.Microservices.Offices.Application.Exceptions;
using InnowiseClinic.Microservices.Offices.Application.Models;

namespace InnowiseClinic.Microservices.Offices.Application.Services.Interfaces;

public interface IOfficeService
{
    /// <exception cref="OfficeNotFoundException"/>
    Task<Office> GetOfficeAsync(Guid id);
    Task<ICollection<OfficePageEntry>> GetOfficePageAsync(int count, int offset);
    Task<Guid> CreateOfficeAsync(OfficeCreationInput input);
    /// <exception cref="OfficeNotFoundException"/>
    Task EditOfficeAsync(Guid id, OfficeEditInput input);
}