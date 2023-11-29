namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// Thrown when a business logic related rule is violated, typically by
/// input data.
/// </summary>
public class BusinessServiceException : BusinessException
{
    /// <summary>
    /// Creates an instance of <see cref="BusinessServiceException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    public BusinessServiceException(string? message) : base(message) { }

    /// <summary>
    /// Creates an instance of <see cref="BusinessServiceException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    /// <param name="innerException">Inner exception.</param>
    public BusinessServiceException(string? message, Exception? innerException) : base(message, innerException) { }
}