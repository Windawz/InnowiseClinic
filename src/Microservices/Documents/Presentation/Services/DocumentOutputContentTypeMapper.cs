
namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class DocumentOutputContentTypeMapper : OutputContentTypeMapper
{
    protected override IReadOnlyDictionary<string, string> CreateContentTypeMap(IEqualityComparer<string> keyComparer)
    {
        return new Dictionary<string, string>(keyComparer)
        {
            [".docx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            [".pdf"] = "application/pdf",
        };
    }
}