using System.Text.RegularExpressions;

namespace _4VGymEF.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhotoUrl { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public ICollection<Activity> Actividades { get; set; } = new List<Activity>();

        public static bool CheckEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool CheckPhone(string phone)
        {
            return !string.IsNullOrWhiteSpace(phone) &&
                Regex.IsMatch(phone, @"^[0-9]{9}$");
        }
    }
}