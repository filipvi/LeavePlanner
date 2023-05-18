using LeavePlanner.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;

namespace LeavePlanner.Models.Validators
{
    public class OibValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string oib = string.Empty;

            //if (validationContext.ObjectInstance.GetType() == typeof(CreateUserViewModel))
            //{
            //    oib = ((CreateUserViewModel)validationContext.ObjectInstance).Oib;
            //}

            var isValid = oib.IsOib();

            return isValid
                ? ValidationResult.Success
                : new ValidationResult("OIB nije u ispravnom formatu!");
        }
    }
}
