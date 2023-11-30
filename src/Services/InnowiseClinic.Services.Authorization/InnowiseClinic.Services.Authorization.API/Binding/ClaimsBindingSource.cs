using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Binding;

public class ClaimsBindingSource : BindingSource
{
    public ClaimsBindingSource() : base(
        id: "Claims",
        displayName: nameof(ClaimsBindingSource),
        isGreedy: false,
        isFromRequest: true)
    { }
}