
namespace InnowiseClinic.Services.Authorization.Services.Services;

public class FailedToLogInException : BusinessServiceException
{
    public FailedToLogInException(string? message) : base(message) { }

    public FailedToLogInException(string? message, Exception? innerException) : base(message, innerException) { }
}