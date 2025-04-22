

using TVStation.Data.Model;

namespace TVStation.Data.DTO.Plans
{
    public class EventReqDTO
    {
        public List<User>? Collaborators { get; set; }
        public Chanel? Chanel { get; set; }
        public string Status { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string RecurrenceRule { get; set; } = string.Empty;
        public bool IsAllday { get; set; }
    }
}
