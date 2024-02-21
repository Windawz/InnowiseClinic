namespace InnowiseClinic.Microservices.Documents.Presentation.Services.Exceptions;

public class UnpermittedDocumentExtensionException(string containerKindName, string? extension)
    : Exception($"Extension '{extension}' is not permitted for a document stored in container of kind '{containerKindName}'");