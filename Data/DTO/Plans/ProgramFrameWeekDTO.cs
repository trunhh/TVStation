namespace TVStation.Data.DTO.Plans
{
    public class ProgramFrameWeekDTO
    {
        public string Sector { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
        public int Year { get; set; }
        public int Week {  get; set; }
    }
}
