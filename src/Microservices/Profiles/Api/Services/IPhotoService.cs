using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Api.Services;

public interface IPhotoService
{
    Task UpdateAsync<TProfile>(Guid id, Stream photoDataInputStream, string? photoExtension)
        where TProfile : Profile;
}