using System.ComponentModel.DataAnnotations;
using TVStation.Data.Constant;
using TVStation.Data.Model;

namespace TVStation.Data.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string? UserName { get; set; }
        public string Name { get; set; } = string.Empty;
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public Guid SiteMapId { get; set; }
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
