using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Validation
{
    public class AlmenoCinqueParole : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;

            string[] source = fieldValue.Trim().Split(" ");

            if (fieldValue == null || source.Count() < 5)
            {
                return new ValidationResult("Il campo deve contenere almeno cinque parole");
            }

            return ValidationResult.Success;
        }
    }
}
