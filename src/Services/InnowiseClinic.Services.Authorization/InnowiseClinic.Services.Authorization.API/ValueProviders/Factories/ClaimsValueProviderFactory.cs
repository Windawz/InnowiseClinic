using System.Security.Claims;
using InnowiseClinic.Services.Authorization.API.BindingSources;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.ValueProviders.Factories;

public class ClaimsValueProviderFactory : IValueProviderFactory
{
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        ClaimsPrincipal? user = context.ActionContext.HttpContext.User;
        if (user is not null)
        {
            var valueProviders = context
                .ActionContext
                .ActionDescriptor
                .Parameters
                .Select(parameter => parameter.BindingInfo?.BindingSource)
                .Where(source => source is not null)
                .OfType<ClaimsBindingSource>()
                .Select(source => new ClaimsValueProvider(user, source));

            foreach (var provider in valueProviders)
            {
                context.ValueProviders.Insert(0, provider);
            }
        }
        return Task.CompletedTask;
    }
}
