using InnowiseClinic.Services.Authorization.API.Filters.ExceptionHandling;
using InnowiseClinic.Services.Authorization.API.ValueProviders.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Services.Authorization.API.Options.Configurators.Mvc;

public class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
{
    private readonly IWebHostEnvironment _environment;

    public ConfigureMvcOptions(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public void Configure(MvcOptions options)
    {
        // Binding from user claims.
        options.ValueProviderFactories.Add(
            new ClaimsValueProviderFactory());

        // Turn layer exceptions into matching error codes.
        options.Filters.Add<ServiceLayerExceptionFilter>();
        options.Filters.Add<APILayerExceptionFilter>();
    }
}
