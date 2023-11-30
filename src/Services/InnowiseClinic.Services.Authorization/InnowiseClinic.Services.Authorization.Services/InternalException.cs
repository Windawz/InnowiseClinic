namespace InnowiseClinic.Services.Authorization.Services;

public class InternalException : ServiceLayerException
{
    public InternalException(string? message) : base(message) { }

    public InternalException(string? message, Exception? innerException) : base(message, innerException) { }
}