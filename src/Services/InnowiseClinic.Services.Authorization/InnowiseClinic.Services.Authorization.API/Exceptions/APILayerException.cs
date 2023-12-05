namespace InnowiseClinic.Services.Authorization.API.Exceptions;

public class APILayerException : Exception
{
    public APILayerException(string? message) : base(message) { }

    public APILayerException(string? message, Exception? innerException) : base(message, innerException) { }
}