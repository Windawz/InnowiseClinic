namespace InnowiseClinic.Services.Authorization.Services.Models;

public class InvalidEmailAddressFormatException : BusinessModelException
{
    public InvalidEmailAddressFormatException(string address) : base($"Format of email address \"{address}\" is invalid")
    {
        Address = address;
    }

    public string Address { get; }
}