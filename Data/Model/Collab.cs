using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TVStation.Data.Model
{
    public class Collab
    {
        public Guid Id { get; set; }
        public string Permission { get; set; } = string.Empty;
        [JsonIgnore]
        public User User { get; set; }
        [NotMapped]
        public string UserName => User?.UserName ?? string.Empty;
        [NotMapped]
        public string SiteMapName => User?.SiteMap?.Name ?? string.Empty;
        [JsonIgnore]
        public Programme Programme { get; set; }
    }
}
