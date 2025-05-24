using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class SimpleReqDTO
    {
        [Required]
        public string Value { get; set; } = string.Empty;
    }
}
