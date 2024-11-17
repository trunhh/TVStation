namespace TVStation.Data.Model
{
    public class Task : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public string Title { get; set; } = string.Empty;
        public string TaskUrl { get; set; } = string.Empty;
        public User? User { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
