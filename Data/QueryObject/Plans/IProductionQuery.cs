namespace TVStation.Data.QueryObject.Plans
{
    public interface IProductionQuery : IPlanQuery
    {
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        Guid? SiteMapId { get; set; }
        string? UserName { get; set; }
    }
}
