using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Binding;

public class ClaimsBindingSource : BindingSource
{
    public ClaimsBindingSource(string claimType) : base(
        id: $"Claims.{claimType}",
        displayName: $"Claims.{claimType}",
        isGreedy: false,
        isFromRequest: true)
    {
        ClaimType = claimType;
    }

    public string ClaimType { get; }
}