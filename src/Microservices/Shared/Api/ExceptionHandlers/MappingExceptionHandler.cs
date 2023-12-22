using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using InnowiseClinic.Microservices.Shared.Api.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace InnowiseClinic.Microservices.Shared.Api.ExceptionHandlers;

public class MappingExceptionHandler : ExceptionHandler
{
    public MappingExceptionHandler(
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<MappingExceptionHandler> logger,
        IWebHostEnvironment environment,
        IOptions<MappingExceptionHandlerOptions> options) : base(problemDetailsFactory, logger, environment)
    {
        _options = options.Value;
    }

    private readonly MappingExceptionHandlerOptions _options;

    protected override int? MapToStatusCode(Exception exception)
    {
        if (TryGetValueMatchingException(exception, _options.MappedStatusCodes.AsReadOnly(), out int statusCode))
        {
            return statusCode;
        }
        else
        {
            return null;
        }
    }

    protected override string? GetSecureMessage(Exception exception)
    {
        if (TryGetValueMatchingException(exception, _options.SecureMessageProviders.AsReadOnly(), out var provider))
        {
            return provider(exception);
        }
        else
        {
            return null;
        }
    }

    private static bool TryGetValueMatchingException<T>(
        Exception exception,
        IReadOnlyDictionary<Type, T> dictionary,
        [MaybeNullWhen(false)] out T value)
    {
        Type? currentExceptionType = exception.GetType();
        value = default;
        
        while (true)
        {
            if (currentExceptionType is null || dictionary.TryGetValue(currentExceptionType, out value))
            {
                break;
            }
            else
            {
                currentExceptionType = currentExceptionType?.BaseType;
            }
        }

        return currentExceptionType is not null;
    }
}
