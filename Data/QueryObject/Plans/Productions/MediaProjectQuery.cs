namespace TVStation.Data.QueryObject.Plans.Productions
{
    public class MediaProjectQuery : IProductionQuery, IPagingQuery
    {
        public string? Keyword { get; set; } = string.Empty;
        public string? Sector { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public bool? IsPersonal { get; set; }
        public int PageIndex { get; set; }
        //public int PageSize { get; set; }
        public DateTime? StartDate { get; set; } = DateTime.MinValue;
        public DateTime? EndDate { get; set; } = DateTime.MaxValue;
        public Guid? SiteMapId { get; set; }
        public string? UserName { get; set; }
        
    }
}
