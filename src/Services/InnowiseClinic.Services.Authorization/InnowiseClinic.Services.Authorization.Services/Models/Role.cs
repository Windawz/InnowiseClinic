using System.Collections.Immutable;

namespace InnowiseClinic.Services.Authorization.Services.Models;

public record Role(
    int Id,
    string Name,
    IImmutableSet<Role> RegisterableRoles);