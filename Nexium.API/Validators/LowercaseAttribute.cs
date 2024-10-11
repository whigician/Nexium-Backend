using System.ComponentModel.DataAnnotations;

namespace Nexium.API.Validators;

public class LowercaseAttribute: ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string stringValue)
        {
            if (stringValue != stringValue.ToLower())
            {
                return new ValidationResult("The field must be in lowercase.");
            }
        }
        return ValidationResult.Success;
    }
}