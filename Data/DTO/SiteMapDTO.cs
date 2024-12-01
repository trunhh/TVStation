using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class SiteMapDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
