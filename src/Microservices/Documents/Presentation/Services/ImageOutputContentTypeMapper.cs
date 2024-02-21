namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class ImageOutputContentTypeMapper : OutputContentTypeMapper
{
    protected override IReadOnlyDictionary<string, string> CreateContentTypeMap(IEqualityComparer<string> keyComparer)
    {
        return new Dictionary<string, string>(keyComparer)
        {
            [".gif"] = "image/gif",
            [".jpeg"] = "image/jpeg",
            [".jpg"] = "image/jpeg",
            [".jfif"] = "image/jpeg",
            [".pjpeg"] = "image/jpeg",
            [".pjp"] = "image/jpeg",
            [".png"] = "image/png",
            [".svg"] = "image/svg+xml",
            [".webp"] = "image/webp",
        };
    }
}