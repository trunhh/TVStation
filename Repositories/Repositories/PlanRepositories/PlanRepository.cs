using TVStation.Data.Constant;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject;
using TVStation.Data.QueryObject.Plans;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories
{
    public abstract class PlanRepository<T, Q> : GenericRepository<T>, IPlanRepository<T,Q>
        where T : class, IEntity, IPlan where Q : class, IPagingQuery, IPlanQuery
    {
        public const int PageSize = 10;
        public PlanRepository(AppDbContext context) : base(context) { }

        public T? SetStatus(Guid id, string status)
        {
            var plan = GetById(id);
            if (plan == null) return null;
            switch (status)
            {
                case PlanStatus.WaitingForApproval:
                    if (plan.Status != PlanStatus.InProgress) return null;
                    break;
                case PlanStatus.Approved:
                    if (plan.Status != PlanStatus.WaitingForApproval) return null;
                    break;
                case PlanStatus.Returned:
                    if (plan.Status != PlanStatus.WaitingForApproval) return null;
                    break;
                case PlanStatus.Retrieved:
                    if (plan.Status != PlanStatus.Approved) return null;
                    break;
                case PlanStatus.Cancelled:
                    break;
                default: return null;
            }
            plan.Status = status;
            _context.SaveChanges();
            return plan;
        }


        public virtual PlanListDTO<T> GetAllPaging(Q query)
        {
            var queryable = GetQueriedData(query);
            queryable = queryable.OrderByDescending(s => s.CreatedDate);
            return new PlanListDTO<T>
            {
                List = queryable.Skip((query.PageIndex - 1) * PageSize).Take(PageSize).ToList(),
                TotalCount = queryable.Count(),
                PageCount = queryable.Count()/PageSize,
                ApprovedCount = queryable.Where(p => p.Status == PlanStatus.Approved).Count(),
                InProgressCount = queryable.Where(p => p.Status == PlanStatus.InProgress).Count(),
                WaitingApprovalCount = queryable.Where(p => p.Status == PlanStatus.WaitingForApproval).Count()
            };
        }

        protected virtual IQueryable<T> GetQueriedData(Q query)
        {
            var queryable = _context.Set<T>().Where(s => s.IsDeleted == false); ;

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

        public IEnumerable<T> GetByStatus(string status)
        {
            return _context.Set<T>().Where(s => s.IsDeleted == false && s.Status == status);
        }
    }
}
