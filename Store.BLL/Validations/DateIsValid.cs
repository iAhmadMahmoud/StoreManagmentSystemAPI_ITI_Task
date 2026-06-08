using System.ComponentModel.DataAnnotations;

namespace Store.BLL
{
    public class DateIsValid : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime expiryDate = Convert.ToDateTime(value);
                if (expiryDate > DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "The Expiry Date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
