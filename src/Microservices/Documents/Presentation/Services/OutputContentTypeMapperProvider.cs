namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class OutputContentTypeMapperProvider : IOutputContentTypeMapperProvider
{
    private static readonly IOutputContentTypeMapper _defaultMapper = new DefaultOutputContentTypeMapper();
    private static readonly IReadOnlyDictionary<ContainerKind, IOutputContentTypeMapper> _mappersPerKind =
        new Dictionary<ContainerKind, IOutputContentTypeMapper>
        {
            [ContainerKind.Photos] = new ImageOutputContentTypeMapper(),
            [ContainerKind.AppointmentResults] = new DocumentOutputContentTypeMapper(),
        };

    public IOutputContentTypeMapper GetForContainerKind(ContainerKind kind)
    {
        if (_mappersPerKind.TryGetValue(kind, out var mapper))
        {
            return mapper;
        }

        return _defaultMapper;
    }

    private class DefaultOutputContentTypeMapper : IOutputContentTypeMapper
    {
        public string MapExtension(string extension)
        {
            return "application/octet-stream";
        }
    }
}