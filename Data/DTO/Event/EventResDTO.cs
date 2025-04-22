namespace TVStation.Data.DTO.Plans
{
    public class EventResDTO
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<string> Attendees { get; set; } = new List<string>();
        public string RecurrenceRule { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string DragBackgroundColor { get; set; } = string.Empty;
        public string BorderColor { get; set; } = string.Empty;
        public bool IsReadOnly { get; set; }
        public bool IsAllday { get; set; }
    }
}
