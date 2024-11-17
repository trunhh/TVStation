using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.Request
{
    public class RegisterReq
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
