namespace TVStation.Data.DTO.Plans
{
    public class PlanDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime Airdate { get; set; }
        public string MediaUrl { get; set; } = string.Empty;
    }
}
