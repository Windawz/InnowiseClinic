namespace InnowiseClinic.Microservices.Shared.Data.Exceptions;

public class DesignTimeDbContextFactoryException : Exception
{
    public DesignTimeDbContextFactoryException(string message) : base(message) { }
}