using System.Diagnostics.CodeAnalysis;
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
        
        do
        {
            if (!dictionary.TryGetValue(currentExceptionType, out value))
            {
                currentExceptionType = currentExceptionType?.BaseType;
            }
        }
        while (currentExceptionType is not null);

        return currentExceptionType is not null;
    }
}
