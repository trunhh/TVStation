namespace TVStation.Data.Model.Plans
{
    public interface IPlan
    {
        User? Creator { get; set; }
        string Sector { get; set; }
        string Status { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        bool IsPersonal { get; set; }
    }
}
