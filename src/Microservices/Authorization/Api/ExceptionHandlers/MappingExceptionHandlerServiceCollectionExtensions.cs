using InnowiseClinic.Microservices.Authorization.Api.Options;

namespace InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;

public static class MappingExceptionHandlerServiceCollectionExtensions
{
    public static IServiceCollection AddMappingExceptionHandler(
        this IServiceCollection serviceCollection,
        Action<MappingExceptionHandlerOptions>? configureOptions = null)
    {
        if (configureOptions is not null)
        {
            serviceCollection.Configure(configureOptions);
        }

        serviceCollection.AddExceptionHandler<MappingExceptionHandler>();

        return serviceCollection;
    }
}