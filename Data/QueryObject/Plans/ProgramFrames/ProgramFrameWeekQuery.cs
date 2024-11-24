namespace TVStation.Data.QueryObject.Plans.ProgramFrames
{
    public class ProgramFrameWeekQuery : IProgramFrameQuery, IPagingQuery
    {
        public string Keyword { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int? Year { get; set; }
        public int? Week { get; set; }
    }
}
