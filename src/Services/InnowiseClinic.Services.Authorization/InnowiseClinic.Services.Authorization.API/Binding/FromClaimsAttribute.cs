using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Binding;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class FromClaimsAttribute : Attribute, IBindingSourceMetadata
{
    public FromClaimsAttribute(string claimType)
    {
        BindingSource = new ClaimsBindingSource(claimType);
    }

    public BindingSource BindingSource { get; }
}