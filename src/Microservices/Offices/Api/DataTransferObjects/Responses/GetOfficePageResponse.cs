namespace InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Responses;

public class GetOfficePageResponse
{
    public required Guid OfficeId { get; init; }
    public string? OfficeNumber { get; init; }
    public required string RegistryPhoneNumber { get; init; }
    public required bool IsActive { get; init; }
}