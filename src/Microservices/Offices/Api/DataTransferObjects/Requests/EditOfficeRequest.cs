namespace InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Requests;

public class EditOfficeRequest
{
    public string? City { get; init; }
    public string? Street { get; init; }
    public string? HouseNumber { get; init; }
    public string? OfficeNumber { get; init; }
    public string? RegistryPhoneNumber { get; init; }
    public bool? IsActive { get; init; }
}