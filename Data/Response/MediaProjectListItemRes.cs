using TVStation.Data.Model;
using TVStation.Data.Model.Plans;

namespace TVStation.Data.Response
{
    public class MediaProjectListItemRes
    {
        public MediaProjectListItemRes(MediaProject original)
        {
            Id = original.Id;
            CreatedDate = original.CreatedDate;
            Creator = original.Creator?.Name ?? "";
            Status = original.Status;
            Title = original.Title;
            MediaUrl = original.MediaUrl;
        } 
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Creator { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
    }
}
