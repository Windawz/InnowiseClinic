namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class DocumentInfo
{
    public DocumentInfo(string? extension)
    {
        Extension = extension;
    }

    public string? Extension { get; }
}