using InnowiseClinic.Services.Authorization.Services;
using InnowiseClinic.Services.Authorization.Services.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Services.Authorization.API.Debugging.Filters;

public class ServiceLayerExceptionFilter : StatusCodeMappingExceptionFilter<ServiceLayerException>
{
    public ServiceLayerExceptionFilter(ProblemDetailsFactory problemDetailsFactory, ILogger<ServiceLayerExceptionFilter> logger)
        : base(problemDetailsFactory, logger) { }

    protected override int? MapExceptionToStatusCode(ServiceLayerException exception)
    {
        return exception switch
        {
            AccountAlreadyExistsException => StatusCodes.Status409Conflict,
            BusinessException => StatusCodes.Status400BadRequest,
            InternalException => StatusCodes.Status500InternalServerError,
            _ => null,
        };
    }
}