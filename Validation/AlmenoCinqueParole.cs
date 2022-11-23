using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Validation
{
    public class AlmenoCinqueParole : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;

            if (fieldValue == null)
            {
                return new ValidationResult("Il campo deve contenere almeno cinque parole");
            }
            
            string[] source = fieldValue.Trim().Split(" ");

            if (source.Count() < 5)
            {
                return new ValidationResult("Il campo deve contenere almeno cinque parole");
            }

            return ValidationResult.Success;
        }
    }
}
