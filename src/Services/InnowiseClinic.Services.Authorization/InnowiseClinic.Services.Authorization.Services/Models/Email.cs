using System.Net.Mail;

namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// A user's email.
/// </summary>
public record Email
{
    private readonly string _address = null!;

    /// <summary>
    /// Creates an instance of <see cref="Email"/>.
    /// </summary>
    /// <param name="address">Email address.</param>
    /// <remarks>
    /// Throws an instance <see cref="InvalidEmailAddressException"/> if
    /// the email address has an invalid format.
    /// </remarks>
    /// <exception cref="InvalidEmailAddressException">
    /// The email address has an invalid format.
    /// </exception>
    public Email(string address)
    {
        Address = address;
    }

    /// <summary>
    /// The address of this email.
    /// </summary>
    /// <remarks>
    /// Throws an instance <see cref="InvalidEmailAddressException"/> on assignment if
    /// the email address has an invalid format.
    /// </remarks>
    /// <exception cref="InvalidEmailAddressException">
    /// The email address has an invalid format.
    /// </exception>
    public string Address
    {
        get => _address;
        init
        {
            if (!IsValid(value))
            {
                throw new InvalidEmailAddressFormatException(value);
            }
            _address = value;
        }
    }

    public static bool IsValid(string address)
    {
        return MailAddress.TryCreate(address, out _);
    }

    private static void ThrowIfInvalidAddress(string address)
    {
        
    }
}