using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class UpdatePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;

    }
}
