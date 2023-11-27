using InnowiseClinic.Services.Authorization.Services;

namespace InnowiseClinic.Services.Authorization.API.Infrastructure;

/// <summary>
/// Catches and maps service layer exceptions to status codes.
/// </summary>
internal class ServiceLayerExceptionFilter : StatusCodeMappingExceptionFilter<ServiceLayerException>
{
    /// <inheritdoc/>
    protected override int? MapExceptionToStatusCode(ServiceLayerException exception)
    {
        return exception switch
        {
            AccountAlreadyExistsException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest,
        };
    }
}