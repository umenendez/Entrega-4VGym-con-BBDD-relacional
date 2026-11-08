using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using _4VGymEF.Validations;

namespace _4VGymEF.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public int? ActivityTypeId { get; set; }

        public int MaxParticipants { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }


        // propiedades de navegacion
        public ICollection<Instructor> ListaInstructores { get; set; } = new List<Instructor>();

        public ActivityType TipoActividad { get; set; } = new ActivityType();

    }

}
