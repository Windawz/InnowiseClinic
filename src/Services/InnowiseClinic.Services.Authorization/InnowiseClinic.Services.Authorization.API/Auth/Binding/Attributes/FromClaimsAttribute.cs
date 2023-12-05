using InnowiseClinic.Services.Authorization.API.Auth.Binding.BindingSources;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Auth.Binding.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class FromClaimsAttribute : Attribute, IBindingSourceMetadata
{
    public FromClaimsAttribute(string claimType)
    {
        BindingSource = new ClaimsBindingSource(claimType);
    }

    public BindingSource BindingSource { get; }
}