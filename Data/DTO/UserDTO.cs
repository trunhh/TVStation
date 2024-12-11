using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class UserDTO
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string Name { get; set; } = string.Empty;
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
