using InnowiseClinic.Microservices.Shared.Api.Options;
using Microsoft.Extensions.DependencyInjection;

namespace InnowiseClinic.Microservices.Shared.Api.ExceptionHandlers;

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