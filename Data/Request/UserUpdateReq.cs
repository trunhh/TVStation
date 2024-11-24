using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.Request
{
    public class UserUpdateReq
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string Name { get; set; } = string.Empty;
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
