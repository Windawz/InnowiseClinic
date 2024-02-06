using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Implementations;

public class ReceptionistProfileService : ProfileService<ReceptionistProfile>, IReceptionistProfileService
{
    public ReceptionistProfileService(IProfileRepository repository) : base(repository) { }
}