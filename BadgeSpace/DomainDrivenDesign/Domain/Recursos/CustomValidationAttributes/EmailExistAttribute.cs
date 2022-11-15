using System.ComponentModel.DataAnnotations;

namespace Domain_Driven_Design.Domain.Recursos.CustomValidationAttributes
{
    public class EmailExistAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => "" == value!.ToString()!.ToUpper()
                ? new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName))
                    : ValidationResult.Success;
    }
}
