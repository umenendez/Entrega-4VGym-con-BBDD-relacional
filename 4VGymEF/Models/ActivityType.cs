using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class ActivityType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int NumberInstructors { get; set; } = 1;
        public ICollection<Activity> listaAtividades { get; set; } = new List<Activity>();

    }

}
