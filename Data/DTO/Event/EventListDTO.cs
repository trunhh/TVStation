using TVStation.Data.Model;

namespace TVStation.Data.DTO.Plans
{
    public class EventListDTO
    {
        public virtual List<EventResDTO> EventList { get; set; } = new List<EventResDTO>();
        public virtual List<Channel> ChannelList { get; set; } = new List<Channel>();
        public virtual int TotalCount { get; set; }
        public virtual int InProgressCount { get; set; }
        public virtual int WaitingApprovalCount { get; set; }
        public virtual int ApprovedCount { get; set; }
    }
}
