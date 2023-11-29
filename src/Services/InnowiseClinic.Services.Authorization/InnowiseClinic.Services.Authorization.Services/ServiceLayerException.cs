namespace InnowiseClinic.Services.Authorization.Services;

public class ServiceLayerException : Exception
{
    /// <summary>
    /// Creates an instance of <see cref="BusinessException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    public ServiceLayerException(string? message) : base(message) { }

    /// <summary>
    /// Creates an instance of <see cref="BusinessException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ServiceLayerException(string? message, Exception? innerException) : base(message, innerException) { }
}