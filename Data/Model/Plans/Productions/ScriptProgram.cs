namespace TVStation.Data.Model.Plans.Productions
{
    public class ScriptProgram : IProduction, IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public User? Creator { get; set; }
        public string Sector { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
        public SiteMap? SiteMap { get; set; }
        public DateTime Airdate { get; set; }
        public string Category {  get; set; } = string.Empty;
    }
}
