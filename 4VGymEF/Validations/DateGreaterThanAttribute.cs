using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace _4VGymEF.Validations
{
    /// <summary>
    /// Valida que una fecha sea posterior a otra propiedad de tipo DateTime del mismo objeto.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Dejamos pasar valores nulos; la responsabilidad de obligar el campo es de [Required]
            if (value is null)
            {
                return ValidationResult.Success;
            }

            // 1. Obtener la propiedad con la que se quiere comparar usando Reflexión
            PropertyInfo? propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (propertyInfo is null)
            {
                return new ValidationResult($"La propiedad con el nombre '{_comparisonProperty}' no existe.");
            }

            // 2. Obtener el valor de la propiedad de comparación del objeto actual
            object? comparisonValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            // 3. Validar si los tipos son DateTime y realizar la comparación
            if (value is DateTime fechaFin && comparisonValue is DateTime fechaInicio)
            {
                if (fechaFin > fechaInicio)
                {
                    return ValidationResult.Success;
                }

                string mensaje = ErrorMessage ?? $"La fecha debe ser posterior a {_comparisonProperty}.";
                return new ValidationResult(mensaje, new[] { validationContext.MemberName! });
            }

            return ValidationResult.Success;
        }
    }
}
