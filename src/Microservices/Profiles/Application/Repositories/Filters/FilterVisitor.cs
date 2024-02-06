namespace InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;

public interface IFilterVisitor
{
    void VisitNameFilter(NameFilter filter);
    void VisitOfficeFilter(OfficeFilter filter);
    void VisitSpecializationFilter(SpecializationFilter filter);
}