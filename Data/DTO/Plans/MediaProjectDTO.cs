
namespace TVStation.Data.DTO.Plans
{
    public class MediaProjectDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
    }
}
