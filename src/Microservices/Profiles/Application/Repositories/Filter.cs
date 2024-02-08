using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Repositories;

public class Filter
{
    public Name? Name { get; init; }
    public Guid? OfficeId { get; init; }
    public Guid? SpecializationId { get; init; }
}