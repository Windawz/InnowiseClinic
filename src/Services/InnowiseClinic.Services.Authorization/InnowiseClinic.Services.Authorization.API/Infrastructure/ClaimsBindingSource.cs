using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Services.Authorization.API.Infrastructure;

/// <summary>
/// A binding source representing binding from user claims.
/// </summary>
internal class ClaimsBindingSource : BindingSource
{
    /// <summary>
    /// Creates an instance of <see cref="ClaimsBindingSource">.
    /// </summary>
    public ClaimsBindingSource() : base("Claims", nameof(ClaimsBindingSource), false, true) { }
}