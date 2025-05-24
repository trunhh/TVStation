using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TVStation.Data.Model
{
    public class Programme : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
        public SiteMap? SiteMap { get; set; }
        [JsonIgnore]
        public User? Owner { get; set; }
        [NotMapped]
        public string UserName => Owner?.UserName ?? string.Empty;
        public List<Collab> Collaborators { get; set; } = new();
        public Channel? Channel { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Script { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<Episode> Episodes { get; set;} = new();
        public bool IsReadOnly { get; set; } = false;
        public DateOnly StartDate {  get; set; } = DateOnly.MinValue;
        public TimeOnly StartTime {  get; set; } = TimeOnly.MinValue;
        public float Duration { get; set; }
        public int EpisodeNumber { get; set; }
        public string Frequency { get; set; } = string.Empty;
    }
}
