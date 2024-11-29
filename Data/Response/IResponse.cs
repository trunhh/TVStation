using TVStation.Data.Constant;

namespace TVStation.Data.Response
{
    public interface IResponse
    {

    }

    public class ListResponse<T> : IResponse
    {
        public virtual IEnumerable<T> List { get; set; } = new List<T>();
        public virtual int PageCount => TotalCount / Config.PageSize;
        public virtual int TotalCount { get; set; }
    }
    public class PlanListRes<T> : ListResponse<T>
    {
        public virtual int InProgressCount { get; set; }
        public virtual int WaitingApprovalCount { get; set; }
        public virtual int ApprovedCount { get; set; }
    }
}
