using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using _4VGymEF.Validations;

namespace _4VGymEF.Models
{
    public class ActivityCreateDto
    {
        public int Id { get; set; }

        [JsonPropertyName("activity_type_id")]
        [Required(ErrorMessage = "30|La tipo de actividad es obligatorio.")]
        public int? ActivityTypeId { get; set; }

        [JsonPropertyName("instructors_id")]
        [Required(ErrorMessage = "26|La lista de monitores no puede ser nula.")]
        [MinLength(1, ErrorMessage = "27|La actividad debe tener asignado al menos un monitor.")]
        public required List<int> InstructorsId { get; set; }

        [JsonPropertyName("max_particpants")]
        [Range(1, 25, ErrorMessage = "25|El aforo máximo debe estar entre 1 y 100 participantes.")]
        public int MaxParticipants { get; set; }

        [JsonPropertyName("date_start")]
        [FutureDate(ErrorMessage = "40|La fecha del evento debe ser posterior a la fecha y hora actual.")]
        [Required(ErrorMessage = "28|La fecha de inicio es obligatoria.")]

        public DateTime DateStart { get; set; }

        [JsonPropertyName("date_end")]
        [Required(ErrorMessage = "29|La fecha de fin es obligatoria.")]
        [DateGreaterThan(nameof(DateStart), ErrorMessage = "41|La fecha de fin debe ser posterior a la fecha de inicio.")]
        public DateTime DateEnd { get; set; }
    }

}
