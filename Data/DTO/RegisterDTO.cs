using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
