using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Auth.Binding;

public class ClaimsValueProvider : BindingSourceValueProvider
{
    private readonly ClaimsPrincipal _user;
    private readonly string _claimType;

    public ClaimsValueProvider(ClaimsPrincipal user, ClaimsBindingSource source) : base(source)
    {
        _user = user;
        _claimType = source.ClaimType;
    }

    public override bool ContainsPrefix(string prefix)
    {
        //return _user.Claims.Any(
        //    claim => claim.Type.Equals(prefix, StringComparison.Ordinal));
        return true;
    }

    public override ValueProviderResult GetValue(string key)
    {
        return new ValueProviderResult(
            _user.FindAll(_claimType)
                .Select(claim => claim.Value)
                .ToArray());
    }
}