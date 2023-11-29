
namespace InnowiseClinic.Services.Authorization.Services;

public class InfrastructureException : ServiceLayerException
{
    public InfrastructureException(string? message) : base(message) { }

    public InfrastructureException(string? message, Exception? innerException) : base(message, innerException) { }
}