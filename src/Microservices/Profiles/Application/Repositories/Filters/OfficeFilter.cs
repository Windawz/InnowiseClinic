namespace InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;

public class OfficeFilter : IFilter
{
    public OfficeFilter(Guid officeId)
    {
        OfficeId = officeId;
    }

    public Guid OfficeId { get; }

    public void Accept(IFilterVisitor visitor)
    {
        visitor.VisitOfficeFilter(this);
    }
}