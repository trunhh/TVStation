using TVStation.Data.Model;

namespace TVStation.Data.DTO.Plans
{
    public class EventResDTO
    {
        public Channel? Channel { private get; set; }
        public Guid CalendarId => Channel?.Id ?? Guid.Empty;
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<string> Attendees => Collaborators?.Select(c => c.UserName).ToList() ?? new List<string>();
        public List<User>? Collaborators { private get; set; }
        public string RecurrenceRule { get; set; } = string.Empty;
        public string Color => Channel?.Color ?? string.Empty;
        public string BgColor => Channel?.BackgroundColor ?? string.Empty;
        public string DragBgColor => Channel?.DragBackgroundColor ?? string.Empty;
        public string BorderColor => Channel?.BorderColor ?? string.Empty;
        public bool IsReadOnly { get; set; }
        public bool IsDeleted { private get; set; }
        public bool IsVisible => !IsDeleted;
        public bool IsAllday { get; set; }
        public string Category => "time";
    }
}
