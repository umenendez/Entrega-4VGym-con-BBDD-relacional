using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class ActivityTypeCreateDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("number-monitors")]
        public int NumberMonitors { get; set; } = 1;

        public ActivityTypeCreateDto()
        {
        }
        public ActivityTypeCreateDto(int id, string name, int numberMonitors)
        {
            Id = id;
            Name = name;
            NumberMonitors = numberMonitors;
        }
    }

}
