using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class InstructorCreateDto
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;


        [JsonPropertyName("photo_url")]
        public string PhotoUrl { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        public InstructorCreateDto()
        {
        }

        public InstructorCreateDto(int id, string name, string email, string photoUrl, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            PhotoUrl = photoUrl;
            Phone = phone;
        }

        public bool isEmpty()
        {
            return (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Email)
                && string.IsNullOrEmpty(PhotoUrl) && string.IsNullOrEmpty(Phone));
        }


    }
}
