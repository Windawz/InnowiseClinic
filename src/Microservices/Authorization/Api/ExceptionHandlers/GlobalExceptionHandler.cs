using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger _logger;

    public GlobalExceptionHandler(ProblemDetailsFactory problemDetailsFactory, ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError($"Exception caught by global handler: \"{exception.Message}\"");

        const int statusCode = StatusCodes.Status500InternalServerError;
        
        var problemDetails = _problemDetailsFactory.CreateProblemDetails(
            httpContext: httpContext,
            statusCode: statusCode);

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}
