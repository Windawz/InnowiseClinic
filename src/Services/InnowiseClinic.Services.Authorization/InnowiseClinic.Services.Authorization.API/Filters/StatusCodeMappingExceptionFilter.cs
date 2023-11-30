using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Services.Authorization.API.Filters;

public abstract class StatusCodeMappingExceptionFilter<TRootException> : IExceptionFilter where TRootException : Exception
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    protected StatusCodeMappingExceptionFilter(ProblemDetailsFactory problemDetailsFactory)
    {
        _problemDetailsFactory = problemDetailsFactory;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        if (exception is TRootException rootException)
        {
            int? statusCode = MapExceptionToStatusCode(rootException);
            if (statusCode is not null)
            {
                var problemDetails = _problemDetailsFactory.CreateProblemDetails(
                    httpContext: context.HttpContext,
                    statusCode: statusCode,
                    detail: exception.Message);

                context.Result = new ObjectResult(problemDetails)
                {
                    StatusCode = statusCode,
                };

                context.ExceptionHandled = true;
            }
        }
    }

    protected abstract int? MapExceptionToStatusCode(TRootException exception);
}
