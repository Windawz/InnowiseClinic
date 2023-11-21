using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Infrastructure;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
internal class FromClaimsAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
{
    private static readonly ClaimsBindingSource _bindingSource = new();
    private readonly string _claimType;

    public FromClaimsAttribute(string claimType)
    {
        _claimType = claimType;
    }

    BindingSource? IBindingSourceMetadata.BindingSource =>
        _bindingSource;

    string? IModelNameProvider.Name =>
        _claimType;
}