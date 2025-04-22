using System.ComponentModel.DataAnnotations.Schema;
using TVStation.Data.Model;

namespace TVStation.Data.QueryObject
{
    public class EventQuery
    {
        public string? Keyword { get; set; } = string.Empty;
        public string? Sector { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;

        //public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? CreatorId { get; set; }
        public List<string>? CollabId { get; set; }
        public string? ChannelId { get; set; }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
