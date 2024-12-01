using System.ComponentModel.DataAnnotations.Schema;

namespace TVStation.Data.Model.Plans
{
    public class MediaProject : IPlan, IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public User? Creator { get; set; }
        [NotMapped]
        public string? CreatorName => Creator?.Name;
        public string Sector { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
        public SiteMap? SiteMap { get; set; }
        public string MediaUrl { get; set; } = string.Empty;
    }
}
