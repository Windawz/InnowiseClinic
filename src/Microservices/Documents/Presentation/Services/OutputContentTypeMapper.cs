namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public abstract class OutputContentTypeMapper : IOutputContentTypeMapper
{
    private readonly IReadOnlyDictionary<string, string> _cachedContentTypeMap;

    public OutputContentTypeMapper()
    {
        _cachedContentTypeMap = CreateContentTypeMap(StringComparer.OrdinalIgnoreCase);
    }
    
    protected virtual string DefaultContentType { get; } = "application/octet-stream";

    public string MapExtension(string extension)
    {
        if (_cachedContentTypeMap.TryGetValue(extension, out var contentType))
        {
            return contentType;
        }

        return DefaultContentType;
    }

    protected abstract IReadOnlyDictionary<string, string> CreateContentTypeMap(IEqualityComparer<string> keyComparer);
}