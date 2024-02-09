using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Application.Services.Implementations;

public class PatientProfileService : ProfileService<PatientProfile>, IPatientProfileService
{
    public PatientProfileService(IProfileRepository repository) : base(repository) { }
}