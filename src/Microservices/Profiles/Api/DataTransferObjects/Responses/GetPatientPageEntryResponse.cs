namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;

public class GetPatientPageEntryResponse
{
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
}