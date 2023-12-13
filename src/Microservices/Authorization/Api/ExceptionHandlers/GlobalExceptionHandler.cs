using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;

public class GlobalExceptionHandler(
    ProblemDetailsFactory problemDetailsFactory,
    ILogger<GlobalExceptionHandler> logger,
    IWebHostEnvironment environment) : ExceptionHandler(problemDetailsFactory, logger, environment)
{
    protected override int? MapToStatusCode(Exception exception)
    {
        Logger.LogError($"Exception caught by global handler: \"{exception.Message}\"");

        return StatusCodes.Status500InternalServerError;
    }
}
