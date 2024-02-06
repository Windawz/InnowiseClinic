namespace InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;

public interface IFilter
{
    void Accept(IFilterVisitor visitor);
}