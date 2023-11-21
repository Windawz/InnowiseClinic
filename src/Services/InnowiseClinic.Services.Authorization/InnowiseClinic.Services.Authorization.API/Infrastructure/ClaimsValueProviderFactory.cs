using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Infrastructure;

/// <summary>
/// Factory for <see cref="ClaimsValueProvider">.
/// </summary>
internal class ClaimsValueProviderFactory : IValueProviderFactory
{
    /// <inheritdoc/>
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        ClaimsPrincipal? principal = context.ActionContext.HttpContext.User;
        context.ValueProviders.Add(
            new ClaimsValueProvider(principal));

        return Task.CompletedTask;
    }
}
