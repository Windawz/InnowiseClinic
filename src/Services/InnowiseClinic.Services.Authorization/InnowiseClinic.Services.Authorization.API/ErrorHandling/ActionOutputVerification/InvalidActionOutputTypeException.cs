using Microsoft.AspNetCore.Mvc.Abstractions;

namespace InnowiseClinic.Services.Authorization.API.ErrorHandling.ActionOutputVerification;

public class InvalidActionOutputTypeException : APILayerException
{
    public InvalidActionOutputTypeException(Type outputType, ActionDescriptor actionDescriptor)
        : base($"Action \"{actionDescriptor.DisplayName}\" attempted to return an instance of non-output type {outputType}")
    {
        OutputType = outputType;
        ActionDescriptor = actionDescriptor;
    }

    public Type OutputType { get; }
    public ActionDescriptor ActionDescriptor { get; }
}