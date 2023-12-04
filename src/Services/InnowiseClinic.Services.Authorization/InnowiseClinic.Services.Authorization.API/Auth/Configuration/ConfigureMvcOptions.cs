using InnowiseClinic.Services.Authorization.API.Auth.Binding;
using InnowiseClinic.Services.Authorization.API.Debugging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Services.Authorization.API.Auth.Configuration;

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
