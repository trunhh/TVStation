using TVStation.Data.Model;

namespace TVStation.Data.DTO.Plans
{
    public class ProgrammeResDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public SiteMap? SiteMap { get; set; }
        public User? Owner { get; set; }
        public List<Collab> Collaborators { get; set; } = new();
        public Channel? Channel { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Script { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<Episode> Episodes { get; set; } = new();
        public bool IsReadOnly { get; set; } = false;
    }
}
