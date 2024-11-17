namespace TVStation.Data.Model.Plans
{
    public interface IProduction : IPlan
    {
        SiteMap? SiteMap { get; set; }
        DateTime Airdate { get; set; }
    }
}
