using InnowiseClinic.Services.Authorization.API.Binding;
using InnowiseClinic.Services.Authorization.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Services.Authorization.API.Configuration;

public class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
    {
        // Binding from user claims.
        options.ValueProviderFactories.Add(
            new ClaimsValueProviderFactory());
        // Turns service layer exceptions into matching error codes.
        options.Filters.Add<ServiceLayerExceptionFilter>();
    }
}
