namespace InnowiseClinic.Microservices.Offices.Api.Services.Interfaces;

public interface IPhotoUploaderService
{
    Task<Guid> UploadAsync(IFormFile photoFormFile);
}