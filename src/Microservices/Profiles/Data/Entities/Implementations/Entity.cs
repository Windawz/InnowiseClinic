using InnowiseClinic.Microservices.Profiles.Data.Entities.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;

public abstract record Entity(Guid Id) : IEntity;