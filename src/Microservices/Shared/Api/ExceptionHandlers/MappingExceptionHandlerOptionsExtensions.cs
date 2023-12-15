using InnowiseClinic.Microservices.Shared.Api.Options;

namespace InnowiseClinic.Microservices.Shared.Api.ExceptionHandlers;

public static class MappingExceptionHandlerOptionsExtensions
{
    public static MappingExceptionHandlerOptions MapStatusCode<TException>(
        this MappingExceptionHandlerOptions options,
        int statusCode) where TException : Exception
    {
        options.MappedStatusCodes[typeof(TException)] = statusCode;

        return options;
    }

    public static MappingExceptionHandlerOptions MapSecureMessage<TException>(
        this MappingExceptionHandlerOptions options,
        Func<TException, string> secureMessageProvider) where TException : Exception
    {
        options.SecureMessageProviders[typeof(TException)] = (Func<Exception, string>)secureMessageProvider;

        return options;
    }
}