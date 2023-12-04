using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InnowiseClinic.Services.Authorization.API.Debugging;

public class APILayerExceptionFilter : StatusCodeMappingExceptionFilter<APILayerException>
{
    public APILayerExceptionFilter(ProblemDetailsFactory problemDetailsFactory, ILogger<APILayerExceptionFilter> logger)
        : base(problemDetailsFactory, logger) { }

    protected override int? MapExceptionToStatusCode(APILayerException exception)
    {
        return exception switch
        {
            _ => null,
        };
    }
}