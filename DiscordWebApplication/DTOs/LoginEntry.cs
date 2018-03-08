using System.ComponentModel.DataAnnotations;

namespace DiscordWebApplication.DTOs
{
    public class LoginEntry
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
