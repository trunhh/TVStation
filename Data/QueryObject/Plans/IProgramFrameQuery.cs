namespace TVStation.Data.QueryObject.Plans
{
    public interface IProgramFrameQuery : IPlanQuery
    {
        int Year { get; set; }
    }
}
