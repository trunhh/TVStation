using TVStation.Data.Model;

namespace TVStation.Data.Request
{
    public class MediaProjectUpdateReq
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
    }
}
