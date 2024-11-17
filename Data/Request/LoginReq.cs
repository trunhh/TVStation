using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.Request
{
    public class LoginReq
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
