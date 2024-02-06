namespace InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;

public class SpecializationFilter : IFilter
{
    public SpecializationFilter(Guid specializationId)
    {
        SpecializationId = specializationId;
    }

    public Guid SpecializationId { get; }

    public void Accept(IFilterVisitor visitor)
    {
        visitor.VisitSpecializationFilter(this);
    }
}