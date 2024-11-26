namespace TVStation.Data.QueryObject
{
    public class UserQuery : IPagingQuery
    {
        public Guid? SiteMapId { get; set; }
        public string? Keyword { get; set; } = string.Empty;
        public int PageIndex { get; set; }
        //public int PageSize { get; set; }
    }
}
