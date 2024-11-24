using TVStation.Data.Model;

namespace TVStation.Data.Request
{
    public class MediaProjectCreateReq
    {
        public string Sector { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
    }
}
