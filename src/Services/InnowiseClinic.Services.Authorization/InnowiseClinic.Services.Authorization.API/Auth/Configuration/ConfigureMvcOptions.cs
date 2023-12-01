using InnowiseClinic.Services.Authorization.API.Auth.Binding;
using InnowiseClinic.Services.Authorization.API.ErrorHandling.ActionOutputVerification;
using InnowiseClinic.Services.Authorization.API.ErrorHandling.ExceptionFilters;
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

        if (_environment.IsDevelopment())
        {
            // Ensures that no object of type that isn't explicitly marked
            // as action output type (such as an output DTO) is returned
            // from actions.
            options.Filters.Add<VerifyActionOutputTypeFilter>();
        }
    }
}
