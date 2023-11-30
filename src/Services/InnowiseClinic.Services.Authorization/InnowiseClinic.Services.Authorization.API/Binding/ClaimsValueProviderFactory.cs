using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Binding;

public class ClaimsValueProviderFactory : IValueProviderFactory
{
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        ClaimsPrincipal? principal = context.ActionContext.HttpContext.User;
        context.ValueProviders.Add(
            new ClaimsValueProvider(principal));

        return Task.CompletedTask;
    }
}
