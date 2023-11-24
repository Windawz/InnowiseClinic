namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Thrown when the format of an email address was invalid.
/// </summary>
public class InvalidEmailAddressFormatException : ModelValidationException
{
    /// <summary>
    /// Creates an instance of <see cref="InvalidEmailAddressFormatException"/>.
    /// </summary>
    /// <param name="address">Email address.</param>
    public InvalidEmailAddressFormatException(string address) : base($"Format of email address \"{address}\" is invalid")
    {
        Address = address;
    }

    /// <summary>
    /// Email address.
    /// </summary>
    public string Address { get; }
}