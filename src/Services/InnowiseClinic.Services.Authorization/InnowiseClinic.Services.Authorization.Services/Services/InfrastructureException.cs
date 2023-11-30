namespace InnowiseClinic.Services.Authorization.Services.Services;

public class InfrastructureServiceException : ServiceLayerException
{
    public InfrastructureServiceException(string? message) : base(message) { }

    public InfrastructureServiceException(string? message, Exception? innerException) : base(message, innerException) { }
}