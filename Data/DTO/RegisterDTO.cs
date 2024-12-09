using System.ComponentModel.DataAnnotations;
using TVStation.Data.Constant;
using TVStation.Data.Model;

namespace TVStation.Data.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public Guid SiteMapId { get; set; }
        [Required]
        public string Role { get; set; } = UserRole.Employee;
    }
}
