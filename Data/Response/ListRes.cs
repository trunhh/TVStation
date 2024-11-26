using TVStation.Data.Constant;

namespace TVStation.Data.Response
{

    public class ListRes<T> : IResponse
    {
        public virtual IEnumerable<T> Data { get; set; } = new List<T>();
        public virtual int PageCount => TotalCount / Config.PageSize;
        public virtual int TotalCount { get; set; }
    }
    public class PlanListRes<T> : ListRes<T>
    {
        public virtual int InProgressCount { get; set; }
        public virtual int WaitingApprovalCount { get; set; }
        public virtual int ApprovedCount { get; set; }
    }
}
