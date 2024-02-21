using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Repositories.Filtering;

public class Filter
{
    public FilteredName? FilteredName { get; init; }
    public Guid? OfficeId { get; init; }
    public Guid? SpecializationId { get; init; }
}