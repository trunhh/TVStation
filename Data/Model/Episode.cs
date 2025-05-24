using System.Text.Json.Serialization;

namespace TVStation.Data.Model
{
    public class Episode : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public Programme Programme { get; set; }
        public int Index { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Script { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsReadOnly { get; set; } = false;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
