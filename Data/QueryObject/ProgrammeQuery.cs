using System.ComponentModel.DataAnnotations.Schema;
using TVStation.Data.Model;

namespace TVStation.Data.QueryObject
{
    public class ProgrammeQuery
    {
        public string? Keyword { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public Guid? SiteMapId { get; set; }
        public Guid? ChannelId { get; set; }
    }
}
