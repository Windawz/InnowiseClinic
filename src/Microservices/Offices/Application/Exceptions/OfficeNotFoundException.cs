namespace InnowiseClinic.Microservices.Offices.Application.Exceptions;

public class OfficeNotFoundException : Exception
{
    public OfficeNotFoundException(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public override string Message =>
        $"Office with id {Id} not found";
}