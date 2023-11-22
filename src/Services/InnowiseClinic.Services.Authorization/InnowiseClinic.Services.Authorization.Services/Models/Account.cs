using System.Collections.Immutable;

namespace InnowiseClinic.Services.Authorization.Services.Models;

public record Account(
    int Id,
    string Email,
    string Password,
    string PhoneNumber,
    bool IsEmailVerified,
    int? PhotoId,
    Account? CreatedBy,
    DateTime CreatedAt,
    Account? UpdatedBy,
    DateTime? UpdatedAt,
    IImmutableSet<Role> Roles);