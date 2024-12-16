namespace TVStation.Data.Model.Plans
{
    public interface IProgramFrame : IPlan
    {
        DateTime Airdate { get; set; }
    }
}
