namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// Thrown when an error occurs in the service layer.
/// </summary>
public class BusinessException : ServiceLayerException
{
    /// <summary>
    /// Creates an instance of <see cref="BusinessException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    public BusinessException(string? message) : base(message) { }

    /// <summary>
    /// Creates an instance of <see cref="BusinessException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    /// <param name="innerException">The inner exception.</param>
    public BusinessException(string? message, Exception? innerException) : base(message, innerException) { }
}