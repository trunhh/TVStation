namespace TVStation.Data.DTO.Plans
{
    public class EventListDTO<T>
    {
        public virtual IEnumerable<T> List { get; set; } = new List<T>();
        public virtual int TotalCount { get; set; }
        public virtual int InProgressCount { get; set; }
        public virtual int WaitingApprovalCount { get; set; }
        public virtual int ApprovedCount { get; set; }
    }
}
