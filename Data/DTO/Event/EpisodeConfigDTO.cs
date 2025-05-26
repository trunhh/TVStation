using System.Text.Json.Serialization;
using TVStation.Data.Model;

namespace TVStation.Data.DTO.Event
{
    public class EpisodeConfigDTO
    {
        public string Script { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
