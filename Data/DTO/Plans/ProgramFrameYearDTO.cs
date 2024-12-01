

namespace TVStation.Data.DTO.Plans
{
    public class ProgramFrameYearDTO
    {
        public string Sector { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
        public int Year { get; set; }
    }
}
