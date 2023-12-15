namespace InnowiseClinic.Microservices.Shared.Api.Options;

public class MappingExceptionHandlerOptions
{
    public IDictionary<Type, int> MappedStatusCodes { get; set; } =
        new Dictionary<Type, int>();

    public IDictionary<Type, Func<Exception, string>> SecureMessageProviders { get; set; } =
        new Dictionary<Type, Func<Exception, string>>();
}