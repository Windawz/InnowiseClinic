using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;

public class NameFilter : IFilter
{
    public NameFilter(Name name)
    {
        Name = name;
    }

    public Name Name { get; }

    public void Accept(IFilterVisitor visitor)
    {
        visitor.VisitNameFilter(this);
    }
}