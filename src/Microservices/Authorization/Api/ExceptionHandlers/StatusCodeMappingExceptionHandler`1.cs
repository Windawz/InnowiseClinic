using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;

public abstract class StatusCodeMappingExceptionHandler<TException> : IExceptionHandler where TException : Exception
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger _logger;

    protected StatusCodeMappingExceptionHandler(
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<StatusCodeMappingExceptionHandler<TException>> logger)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is TException specificException)
        {
            _logger.LogError(exception, $"Exception caught by {GetType().Name}: \"{exception.Message}\"");

            int statusCode = MapToStatusCode(specificException);
            string? detailMessage = GetDetailMessage(specificException);

            var problemDetails = _problemDetailsFactory.CreateProblemDetails(
                httpContext: httpContext,
                statusCode: statusCode,
                detail: detailMessage);
            
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
        else
        {
            return false;
        }
    }

    protected abstract int MapToStatusCode(TException exception);

    protected virtual string? GetDetailMessage(TException exception) => null;
}
