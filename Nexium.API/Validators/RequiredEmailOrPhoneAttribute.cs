using System.ComponentModel.DataAnnotations;
using Nexium.API.Entities;

namespace Nexium.API.Validators;

public class RequiredEmailOrPhoneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var userAccount = (UserAccount)validationContext.ObjectInstance;

        if (string.IsNullOrWhiteSpace(userAccount.Email) && string.IsNullOrWhiteSpace(userAccount.PhoneNumber))
            return new ValidationResult("At least one of Email or PhoneNumber must be provided.");

        return ValidationResult.Success;
    }
}