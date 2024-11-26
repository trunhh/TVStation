namespace TVStation.Data.QueryObject.Plans.ProgramFrames
{
    public class ProgramFrameBroadcastQuery : IProgramFrameQuery, IPagingQuery
    {
        public string? Keyword { get; set; } = string.Empty;
        public string? Sector { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public bool? IsPersonal { get; set; }
        public int PageIndex { get; set; }
        //public int PageSize { get; set; }
        public int? Year { get; set; }
        public DateTime? StartDate { get; set; } = DateTime.MinValue;
        public DateTime? EndDate { get; set; } = DateTime.MaxValue;
    }
}
