namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Thrown when an operation is performed on or a value is used in the construction of
/// a business model object that would otherwise cause violation of business rules.
/// </summary>
public class ModelValidationException : ServiceLayerException
{
    /// <summary>
    /// Creates an instance of <see cref="ModelValidationException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    public ModelValidationException(string? message) : base(message) { }

    /// <summary>
    /// Creates an instance of <see cref="ModelValidationException"/>.
    /// </summary>
    /// <param name="message">Exception summary message.</param>
    /// <param name="innerException">Inner exception.</param>
    public ModelValidationException(string? message, Exception? innerException) : base(message, innerException) { }
}