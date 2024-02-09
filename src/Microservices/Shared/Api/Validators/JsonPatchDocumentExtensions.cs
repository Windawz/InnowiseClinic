using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnowiseClinic.Microservices.Shared.Api.Validators;

public static class JsonPatchDocumentExtensions
{
    public static void ApplyToAndValidate<TTarget>(
        this JsonPatchDocument<TTarget> patchDocument,
        TTarget target,
        IValidator<TTarget> validator,
        ModelStateDictionary modelState) where TTarget : class
    {
        ApplyPatch(patchDocument, target);

        var validationResult = validator.Validate(target);
        
        AddValidationErrorsToModelState(validationResult, modelState);
    }

    public static async Task ApplyToAndValidateAsync<TTarget>(
        this JsonPatchDocument<TTarget> patchDocument,
        TTarget target,
        IValidator<TTarget> validator,
        ModelStateDictionary modelState) where TTarget : class
    {
        ApplyPatch(patchDocument, target);

        var validationResult = await validator.ValidateAsync(target);

        AddValidationErrorsToModelState(validationResult, modelState);
    }

    private static void ApplyPatch<TTarget>(JsonPatchDocument<TTarget> patchDocument, TTarget target)
        where TTarget : class
    {
        patchDocument.ApplyTo(target);
    }

    private static void AddValidationErrorsToModelState(ValidationResult result, ModelStateDictionary modelState)
    {
        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}