using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace InnowiseClinic.Microservices.Profiles.Api.Attributes;

/// <summary>
/// Makes the action only get called if any parameter with one of the
/// provided names is present in the query section of the URL.
/// <para/>
/// Its main purpose is
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
    private readonly string[] _parameterNames;

    public QueryParameterConstraintAttribute(params string[] parameterNames)
    {
        _parameterNames = parameterNames;
    }

    public int Order => default;

    public bool Accept(ActionConstraintContext context)
    {
        var queryParameterKeys = context.RouteContext
            .HttpContext
            .Request
            .Query
            .Keys;

        return _parameterNames.Any(name => queryParameterKeys.Contains(name));
    }
}