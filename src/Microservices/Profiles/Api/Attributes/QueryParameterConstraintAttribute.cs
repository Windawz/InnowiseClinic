using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace InnowiseClinic.Microservices.Profiles.Api.Attributes;

/// <summary>
/// An action constraint attribute, whose main purpose is
/// to make possible controller action overloading based on
/// what parameters are present.
/// <para/>
/// Mainly useful for making it easier to keep the API structure RESTful when providing
/// endpoints for calling specific get-methods on domain layer services
/// that return filtered results.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class QueryParameterConstraintAttribute : Attribute, IActionConstraint
{
    private readonly string _parameterName;

    public QueryParameterConstraintAttribute(string parameterName)
    {
        _parameterName = parameterName;
    }

    public int Order => default;

    public bool Accept(ActionConstraintContext context)
    {
        return context.RouteContext
            .HttpContext
            .Request
            .Query
            .Keys
            .Contains(_parameterName, StringComparer.InvariantCultureIgnoreCase);
    }
}