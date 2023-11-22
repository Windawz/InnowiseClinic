using System.Collections.Immutable;

namespace InnowiseClinic.Services.Authorization.Services.Models;

public record Role(
    string Name,
    IImmutableSet<Role> RegisterableRoles);