namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class ContainerKind : IEquatable<ContainerKind>
{
    public static readonly ContainerKind Photos = new("photos",
        ".jpg",
        ".jpeg",
        ".png",
        ".tga");

    public static readonly ContainerKind AppointmentResults = new("appointmentResults",
        ".docx",
        ".pdf");

    private ContainerKind(string name, params string[] permittedExtensions)
    {
        Name = name.ToLower();
        PermittedExtensions = permittedExtensions;
    }

    public string Name { get; }
    public IReadOnlyCollection<string> PermittedExtensions { get; }

    public bool Equals(ContainerKind? other)
    {
        return other is not null
            && Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        return obj is ContainerKind kind
            && Equals(kind);   
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode(StringComparison.OrdinalIgnoreCase);
    }
}