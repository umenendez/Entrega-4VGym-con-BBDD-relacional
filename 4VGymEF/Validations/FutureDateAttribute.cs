using System.ComponentModel.DataAnnotations;

namespace _4VGymEF.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Si el valor es nulo, dejamos que la responsabilidad de requerirlo sea del atributo [Required]
            if (value is null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime fechaIngresada)
            {
                // Comparación directa con la fecha/hora actual del sistema
                if (fechaIngresada > DateTime.Now)
                {
                    return ValidationResult.Success;
                }

                // Error personalizado si la fecha no es futura
                string mensajeError = ErrorMessage ?? "La fecha debe ser posterior al momento actual.";
                return new ValidationResult(mensajeError);
            }

            return new ValidationResult("El formato de fecha no es válido.");
        }
    }
}
