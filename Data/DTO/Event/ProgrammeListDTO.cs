using TVStation.Data.Model;

namespace TVStation.Data.DTO.Plans
{
    public class ProgrammeListDTO
    {
        public virtual List<Programme> List { get; set; } = new List<Programme>();
        public virtual int TotalCount { get; set; }
        public virtual int InProgressCount { get; set; }
        public virtual int WaitingApprovalCount { get; set; }
        public virtual int ApprovedCount { get; set; }
    }
}
