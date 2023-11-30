using InnowiseClinic.Services.Authorization.Services;
using InnowiseClinic.Services.Authorization.Services.Services;

namespace InnowiseClinic.Services.Authorization.API.Filters;

public class ServiceLayerExceptionFilter : StatusCodeMappingExceptionFilter<ServiceLayerException>
{
    protected override int? MapExceptionToStatusCode(ServiceLayerException exception)
    {
        return exception switch
        {
            AccountAlreadyExistsException => StatusCodes.Status409Conflict,
            BusinessException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };
    }
}