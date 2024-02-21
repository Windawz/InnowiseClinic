namespace InnowiseClinic.Microservices.Shared.Api.Contracts;

public class ProfilePhotoUpdated
{
    public required Guid AccountId { get; set; }
    public required string? Extension { get; set; }
    public required byte[] PhotoData { get; set; }
}