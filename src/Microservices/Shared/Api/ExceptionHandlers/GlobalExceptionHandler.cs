using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace InnowiseClinic.Microservices.Shared.Api.ExceptionHandlers;

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
