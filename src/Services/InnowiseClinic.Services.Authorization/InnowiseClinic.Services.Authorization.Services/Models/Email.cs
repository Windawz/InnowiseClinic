using System.Net.Mail;

namespace InnowiseClinic.Services.Authorization.Services.Models;

public record Email
{
    private readonly string _address = null!;

    public Email(string address)
    {
        Address = address;
    }

    public string Address
    {
        get => _address;
        init
        {
            value = value.Trim();
            if (!IsValid(value))
            {
                throw new InvalidEmailAddressFormatException(value);
            }
            _address = value;
        }
    }

    public static bool IsValid(string address)
    {
        return !string.IsNullOrEmpty(address);
    }
}