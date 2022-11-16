using Domain_Driven_Design.Infra;
using System.ComponentModel.DataAnnotations;

namespace Domain_Driven_Design.Domain.Recursos.CustomValidationAttributes
{
    public class EmailExistAttribute : ValidationAttribute
    {
        //private readonly ApplicationDbContext _context;
        
        //protected EmailExistAttribute() { }
        //public EmailExistAttribute(ApplicationDbContext context) => _context = context;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            => "Consulta do banco" == value!.ToString()!.ToUpper()
                ? new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName))
                    : ValidationResult.Success;
    }
}
