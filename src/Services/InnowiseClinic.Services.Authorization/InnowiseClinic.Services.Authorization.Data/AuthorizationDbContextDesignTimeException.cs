namespace InnowiseClinic.Services.Authorization.Data;

/// <summary>
/// Thrown by <see cref="AuthorizationDbContextDesignTimeFactory"/> if
/// context creation fails for whatever reason.
/// </summary>
public class AuthorizationDbContextDesignTimeException : Exception
{
    /// <summary>
    /// Creates an instance of <see cref="AuthorizationDbContextDesignTimeException"/>.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AuthorizationDbContextDesignTimeException(string message) : base(message) { }
}