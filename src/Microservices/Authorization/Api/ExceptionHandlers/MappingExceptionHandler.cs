using InnowiseClinic.Microservices.Authorization.Api.Options;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;

public class MappingExceptionHandler : ExceptionHandler
{
    private readonly MappingExceptionHandlerOptions _options;

    public MappingExceptionHandler(
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<MappingExceptionHandler> logger,
        IWebHostEnvironment environment,
        IOptions<MappingExceptionHandlerOptions> options) : base(problemDetailsFactory, logger, environment)
    {
        _options = options.Value;
    }

    protected override int? MapToStatusCode(Exception exception)
    {
        return GetValueMatchingException(exception, _options.MappedStatusCodes.AsReadOnly());
    }

    protected override string? GetSecureMessage(Exception exception)
    {
        return GetValueMatchingException(exception, _options.SecureMessageProviders.AsReadOnly())?
            .Invoke(exception);
    }

    private static T? GetValueMatchingException<T>(Exception exception, IReadOnlyDictionary<Type, T> dictionary)
    {
        Type? currentExceptionType = exception.GetType();
        T? value;
        
        do
        {
            if (!dictionary.TryGetValue(currentExceptionType, out value))
            {
                currentExceptionType = currentExceptionType?.BaseType;
            }
        }
        while (currentExceptionType is not null);

        return value;
    }
}
