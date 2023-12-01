using InnowiseClinic.Services.Authorization.API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Services.Authorization.API.ErrorHandling.ExceptionFilters;

public abstract class StatusCodeMappingExceptionFilter<TRootException> : IExceptionFilter where TRootException : Exception
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger _logger;

    protected StatusCodeMappingExceptionFilter(
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<StatusCodeMappingExceptionFilter<TRootException>> logger)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        if (exception is TRootException rootException)
        {
            int? statusCode = MapExceptionToStatusCode(rootException);
            if (statusCode is not null)
            {
                if (StatusCodeCategory.ServerError.Includes(statusCode.Value))
                {
                    _logger.LogError(rootException.ToString());
                }

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
