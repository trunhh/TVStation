namespace TVStation.Data.DTO.Plans
{
    public class PlanNotiDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
