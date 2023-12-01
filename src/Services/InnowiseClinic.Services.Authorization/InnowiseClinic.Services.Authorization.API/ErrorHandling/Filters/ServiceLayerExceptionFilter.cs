using InnowiseClinic.Services.Authorization.Services;
using InnowiseClinic.Services.Authorization.Services.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Services.Authorization.API.ErrorHandling.Filters;

public class ServiceLayerExceptionFilter : StatusCodeMappingExceptionFilter<ServiceLayerException>
{
    public ServiceLayerExceptionFilter(ProblemDetailsFactory problemDetailsFactory) : base(problemDetailsFactory) { }

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