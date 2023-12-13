using System.Diagnostics.CodeAnalysis;
using InnowiseClinic.Microservices.Authorization.Api.Options;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;

public class MappingExceptionHandler(
    ProblemDetailsFactory problemDetailsFactory,
    ILogger<MappingExceptionHandler> logger,
    IWebHostEnvironment environment,
    IOptions<MappingExceptionHandlerOptions> options) : ExceptionHandler(problemDetailsFactory, logger, environment)
{
    private readonly MappingExceptionHandlerOptions _options = options.Value;

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
