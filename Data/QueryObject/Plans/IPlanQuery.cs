namespace TVStation.Data.QueryObject.Plans
{
    public interface IPlanQuery
    {
        string? Keyword { get; set; }
        string? Sector { get; set; }
        string? Status { get; set; }
        bool? IsPersonal { get; set; }
    }
}
