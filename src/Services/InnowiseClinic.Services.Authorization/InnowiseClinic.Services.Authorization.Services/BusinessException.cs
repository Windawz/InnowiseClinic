namespace InnowiseClinic.Services.Authorization.Services;

public class BusinessException : ServiceLayerException
{
    public BusinessException(string? message) : base(message) { }

    public BusinessException(string? message, Exception? innerException) : base(message, innerException) { }
}