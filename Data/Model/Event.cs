using System.ComponentModel.DataAnnotations.Schema;

namespace TVStation.Data.Model
{
    public class Event : IEntity
    {
        public Guid Id { get; set; }
        //Non-display-------------------------------------------
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public User? Creator { get; set; }
        public List<User>? Collaborators { get; set; }
        public Chanel? Chanel { get; set; }
        public string Status { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        //------------------------------------------------------
        [NotMapped]
        public string Body => Summary;
        [NotMapped]
        public List<string> Attendees => Collaborators?.Select(c => c.Name).ToList() ?? new List<string>();

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string RecurrenceRule { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string DragBackgroundColor { get; set; } = string.Empty;
        public string BorderColor { get; set; } = string.Empty;
        public bool IsReadOnly { get; set; }
        public bool IsAllday { get; set; }
        
    }
}
