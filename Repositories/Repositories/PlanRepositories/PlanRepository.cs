using TVStation.Data.Constant;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject;
using TVStation.Data.QueryObject.Plans;
using TVStation.Data.Response;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories
{
    public abstract class PlanRepository<T, Q> : GenericRepository<T, Q>
        where T : class, IEntity, IPlan where Q : class, IPagingQuery, IPlanQuery
    {
        public PlanRepository(AppDbContext context) : base(context) { }

        public override IResponse GetAll(Q query)
        {
            var queryable = GetQueriedData(query);
            queryable = queryable.OrderByDescending(s => s.CreatedDate);
            return new PlanListRes<T>
            {
                Data = queryable.Skip((query.PageIndex - 1) * Config.PageSize).Take(Config.PageSize).ToList(),
                TotalCount = queryable.Count(),
                ApprovedCount = queryable.Where(p => p.Status == PlanStatus.Approved).Count(),
                InProgressCount = queryable.Where(p => p.Status == PlanStatus.InProgress).Count(),
                WaitingApprovalCount = queryable.Where(p => p.Status == PlanStatus.WaitingForApproval).Count()
            };
        }

        protected override IQueryable<T> GetQueriedData(Q query)
        {
            var queryable = base.GetQueriedData(query);

            if (!string.IsNullOrEmpty(query.Keyword))
            {
                queryable = queryable.Where(s => s.Title.Contains(query.Keyword));
            }

            if (!string.IsNullOrEmpty(query.Sector))
            {
                queryable = queryable.Where(s => s.Sector == query.Sector);
            }

            if (!string.IsNullOrEmpty(query.Status))
            {
                queryable = queryable.Where(s => s.Status == query.Status);
            }

            if (query.IsPersonal == true)
            {
                queryable = queryable.Where(s => s.IsPersonal == true);
            }
            return queryable;
        }
    }
}
