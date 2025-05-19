namespace TVStation.Data.Model
{
    public class Channel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string DragBackgroundColor { get; set; } = string.Empty;
        public string BorderColor { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
