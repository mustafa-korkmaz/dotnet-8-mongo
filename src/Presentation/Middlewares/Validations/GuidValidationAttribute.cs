using System.ComponentModel.DataAnnotations;

namespace Presentation.Middlewares.Validations
{
    public class GuidValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            return Guid.TryParse(value.ToString(), out _);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} field does not have an applicable ID";
        }
    }
}
