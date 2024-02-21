
using InnowiseClinic.Microservices.Profiles.Application.Exceptions;
using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories;
using InnowiseClinic.Microservices.Shared.Api.Contracts;
using MassTransit;

namespace InnowiseClinic.Microservices.Profiles.Api.Services;

public class PhotoService : IPhotoService
{
    private readonly IProfileRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public PhotoService(IProfileRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task UpdateAsync<TProfile>(Guid id, Stream photoDataInputStream, string? photoExtension)
        where TProfile : Profile
    {
        var profile = await _repository.GetAsync<TProfile>(id)
            ?? throw new ProfileNotFoundByIdException(id);
        
        Guid accountId = profile.AccountId;
        
        await using var photoDataOutputStream = new MemoryStream();
        await photoDataInputStream.CopyToAsync(photoDataOutputStream);

        byte[] photoData = photoDataOutputStream.ToArray();

        var message = new ProfilePhotoUpdated()
        {
            AccountId = accountId,
            Extension = photoExtension,
            PhotoData = photoData,
        };

        await _publishEndpoint.Publish(message);
    }
}