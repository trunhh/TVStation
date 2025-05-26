namespace TVStation.Data.QueryObject
{
    public class EpisodeQuery
    {
        public Guid ChannelId { get; set; }
        public DateOnly Date { get; set; }
    }
}
