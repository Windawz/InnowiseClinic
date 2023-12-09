using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;

public abstract class ExceptionHandler : IExceptionHandler
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    protected ExceptionHandler(
        ProblemDetailsFactory problemDetailsFactory,
        ILogger logger,
        IWebHostEnvironment environment)
    {
        _problemDetailsFactory = problemDetailsFactory;
        Logger = logger;
        IsDevelopment = environment.IsDevelopment();
    }

    protected ILogger Logger { get; }
    protected bool IsDevelopment { get; }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (MapToStatusCode(exception) is int statusCode)
        {
            var problemDetails = _problemDetailsFactory.CreateProblemDetails(
                httpContext: httpContext,
                statusCode: statusCode,
                detail: GetMessage(exception));

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            
            return true;
        }
        else
        {
            return false;
        }
    }

    protected abstract int? MapToStatusCode(Exception exception);

    protected virtual string? GetSecureMessage(Exception exception) =>
        null;

    private string? GetMessage(Exception exception)
    {
        if (IsDevelopment)
        {
            return exception.Message;
        }
        else
        {
            return GetSecureMessage(exception);
        }
    }
}
