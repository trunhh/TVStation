namespace TVStation.Data.DTO.Plans
{
    public class ProgramFrameBroadcastDTO
    {
        public string Sector { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
        public DateTime Airdate { get; set; }
    }
}
