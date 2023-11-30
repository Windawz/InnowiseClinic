namespace InnowiseClinic.Services.Authorization.Services.Models;

public class BusinessModelException : BusinessException
{
    public BusinessModelException(string? message) : base(message) { }

    public BusinessModelException(string? message, Exception? innerException) : base(message, innerException) { }
}