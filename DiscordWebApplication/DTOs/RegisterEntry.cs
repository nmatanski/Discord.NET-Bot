using System.ComponentModel.DataAnnotations;

namespace DiscordWebApplication.DTOs
{
    public class RegisterEntry
    {
        [Required]
        public string DiscordUsername { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
