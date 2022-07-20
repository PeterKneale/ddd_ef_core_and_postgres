using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo.Pages;

public static class PageValidationExtensions 
{
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState) 
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