using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Binding;

public class ClaimsValueProvider : IValueProvider
{
    private readonly ClaimsPrincipal? _principal;

    public ClaimsValueProvider(ClaimsPrincipal? principal)
    {
        _principal = principal;
    }

    public bool ContainsPrefix(string prefix)
    {
        return _principal?.FindFirst(type: prefix) is not null;
    }

    public ValueProviderResult GetValue(string key)
    {
        if (_principal is null)
        {
            return ValueProviderResult.None;
        }
        else
        {
            return new ValueProviderResult(
                _principal.FindAll(key)
                    .Select(claim => claim.Value)
                    .ToArray());
        }
    }
}