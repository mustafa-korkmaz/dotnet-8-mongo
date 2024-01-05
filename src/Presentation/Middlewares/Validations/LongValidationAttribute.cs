using System.ComponentModel.DataAnnotations;

namespace Presentation.Middlewares.Validations
{
    public class LongValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            var valid = long.TryParse(value.ToString(), out var id);

            if (!valid) return false;

            return id > 0;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} field does not have an applicable ID";
        }
    }
}
