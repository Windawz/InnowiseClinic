namespace InnowiseClinic.Services.Authorization.Services;

public class ServiceLayerException : Exception
{
    public ServiceLayerException(string? message) : base(message) { }

    public ServiceLayerException(string? message, Exception? innerException) : base(message, innerException) { }
}