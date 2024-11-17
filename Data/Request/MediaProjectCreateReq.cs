using TVStation.Data.Model;

namespace TVStation.Data.Request
{
    public class MediaProjectCreateReq
    {
        public string Sector { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
        public DateTime Airdate { get; set; }
        public string MediaUrl { get; set; } = string.Empty;
    }
}
