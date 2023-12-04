namespace InnowiseClinic.Services.Authorization.API.Debugging;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DisableActionVerificationAttribute : Attribute { }