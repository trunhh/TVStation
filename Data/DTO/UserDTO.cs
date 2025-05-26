using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TVStation.Data.Model;

namespace TVStation.Data.DTO
{
    public class UserDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; } = string.Empty;
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid? SiteMapId {  get; set; }
        public string? UserName { get; set; }
    }
}
