using System.Text.Json.Serialization;
using TVStation.Data.Model;

namespace TVStation.Data.DTO.Event
{
    public class ProgrammeConfigDTO
    {

        public Channel? Channel { get; set; }
        public Guid ChannelId { get; set; }
        public SiteMap? SiteMap { get; set; }
        public Guid SiteMapId { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public float Duration { get; set; }
        public int EpisodeNumber { get; set; }
        public string Frequency { get; set; } = string.Empty;
    }
}
