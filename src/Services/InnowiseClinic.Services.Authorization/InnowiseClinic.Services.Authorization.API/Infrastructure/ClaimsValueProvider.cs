using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Infrastructure;

/// <summary>
/// Performs model binding based on claims of <see cref="HttpContext.User"/>.
/// </summary>
internal class ClaimsValueProvider : IValueProvider
{
    private readonly ClaimsPrincipal? _principal;

    /// <summary>
    /// Creates an instance of <see cref="ClaimsValueProvider"/>
    /// that extracts its values from user claims.
    /// </summary>
    /// <param name="principal">A <see cref="ClaimsPrincipal"> representing the user, if any.</param>
    public ClaimsValueProvider(ClaimsPrincipal? principal)
    {
        _principal = principal;
    }

    /// <summary>
    /// <inheritdoc/>
    /// <para/>
    /// This implementation only checks if the principal
    /// contains a claim of type equal to <paramref name="prefix"/>.
    /// </summary>
    /// <param name="prefix"><inheritdoc/></param>
    /// <returns>
    /// True if the principal contains a claim of type
    /// equal to <paramref name="prefix"/>, false otherwise.
    /// </returns>
    public bool ContainsPrefix(string prefix)
    {
        return _principal?.FindFirst(type: prefix) is not null;
    }

    /// <summary>
    /// Returns the value of the claim of type equal to
    /// <paramref name="key"/>, or nothing if the principal is null.
    /// </summary>
    /// <param name="key">The claim type to get the value of.</param>
    /// <returns>
    /// Returns the value of the claim of type equal to
    /// <paramref name="key"/>, or nothing if the principal is null.
    /// </returns>
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