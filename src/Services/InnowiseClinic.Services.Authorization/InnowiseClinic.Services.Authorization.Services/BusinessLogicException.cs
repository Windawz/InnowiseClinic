namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// Thrown when a business logic related rule is violated, typically by
/// input data.
/// </summary>
public class BusinessLogicException : ServiceLayerException
{
    /// <summary>
    /// Creates an instance of <see cref="BusinessLogicException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    public BusinessLogicException(string? message) : base(message) { }

    /// <summary>
    /// Creates an instance of <see cref="BusinessLogicException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    /// <param name="innerException">Inner exception.</param>
    public BusinessLogicException(string? message, Exception? innerException) : base(message, innerException) { }
}