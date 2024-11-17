namespace TVStation.Data.Model.Plans.ProgramFrames
{
    public class ProgramFrameBroadcast : IProgramFrame, IEntity
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
        public int Year { get; set; }
        public DateTime Airdate { get; set; }
    }
}
