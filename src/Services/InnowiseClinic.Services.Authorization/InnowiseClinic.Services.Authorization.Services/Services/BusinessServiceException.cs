namespace InnowiseClinic.Services.Authorization.Services.Services;

public class BusinessServiceException : BusinessException
{
    public BusinessServiceException(string? message) : base(message) { }

    public BusinessServiceException(string? message, Exception? innerException) : base(message, innerException) { }
}