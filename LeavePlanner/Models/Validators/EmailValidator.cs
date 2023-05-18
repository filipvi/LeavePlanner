using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace LeavePlanner.Models.Validators
{
    public class EmailValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = string.Empty;

            //if (validationContext.ObjectInstance.GetType() == typeof(CreateUserViewModel))
            //{
            //    email = ((CreateUserViewModel)validationContext.ObjectInstance).Email;
            //}

            if (!MailAddress.TryCreate(email, out var mailAddress))
            {
                return new ValidationResult("E-mail nije ispravan!");
            }

            var hostParts = mailAddress.Host.Split('.');

            if (hostParts.Length == 1)
            {
                // No dot.
                return new ValidationResult("E-mail nije ispravan!");
            }

            if (hostParts.Any(p => p == string.Empty))
            {
                // Double dot
                return new ValidationResult("E-mail nije ispravan!");
            }

            if (hostParts[^1].Length < 2)
            {
                // TLD only one letter.
                return new ValidationResult("E-mail nije ispravan!");
            }

            if (mailAddress.User.Contains(' '))
            {
                // Empty space
                return new ValidationResult("E-mail nije ispravan!");
            }

            if (mailAddress.User.Split('.').Any(p => p == string.Empty))
            {
                // Double dot or dot at end of user part.
                return new ValidationResult("E-mail nije ispravan!");
            }

            return ValidationResult.Success;
        }
    }
}