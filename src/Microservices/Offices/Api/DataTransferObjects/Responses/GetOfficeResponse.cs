namespace InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Responses;

public class GetOfficeResponse
{
    public Guid Id { get; init; }
    public required string City { get; init; }
    public required string Street { get; init; }
    public required string HouseNumber { get; init; }
    public string? OfficeNumber { get; init; }
    public required string RegistryPhoneNumber { get; init; }
    public required bool IsActive { get; init; }
}