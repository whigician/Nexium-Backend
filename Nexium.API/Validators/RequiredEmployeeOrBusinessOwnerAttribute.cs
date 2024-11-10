using System.ComponentModel.DataAnnotations;
using Nexium.API.Entities;

namespace Nexium.API.Validators;

public class RequiredEmployeeOrBusinessOwnerAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var businessMember = (BusinessMember)validationContext.ObjectInstance;

        if ((!businessMember.BusinessOwnerId.HasValue && !businessMember.EmployeeId.HasValue)
            || (businessMember.BusinessOwnerId.HasValue && businessMember.EmployeeId.HasValue))
            return new ValidationResult("Only the business owner or the employee must be provided.");

        return ValidationResult.Success;
    }
}