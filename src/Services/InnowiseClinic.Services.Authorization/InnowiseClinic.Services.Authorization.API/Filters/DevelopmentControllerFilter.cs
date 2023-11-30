using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Services.Authorization.API.Filters;

public class DevelopmentControllerFilterAttribute : ActionFilterAttribute
{
    private readonly bool _isDevelopment;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public DevelopmentControllerFilterAttribute(IWebHostEnvironment environment, ProblemDetailsFactory problemDetailsFactory)
    {
        _isDevelopment = environment.IsDevelopment();
        _problemDetailsFactory = problemDetailsFactory;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!_isDevelopment)
        {
            int statusCode = StatusCodes.Status404NotFound;

            var problemDetails = _problemDetailsFactory.CreateProblemDetails(
                httpContext: context.HttpContext,
                statusCode: statusCode);

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = statusCode,
            };
        }
        base.OnActionExecuting(context);
    }
}