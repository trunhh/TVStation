using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class SiteMapCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
