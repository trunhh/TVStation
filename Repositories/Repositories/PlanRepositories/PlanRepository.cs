using Microsoft.EntityFrameworkCore;
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

        public virtual PlanListDTO<T> GetAllPaging(Q query)
        {
            var queryable = GetQueriedData(query);
            queryable = queryable.OrderByDescending(s => s.CreatedDate);
            return new PlanListDTO<T>
            {
                List = queryable.Skip((query.PageIndex - 1) * PageSize).Take(PageSize).ToList(),
                TotalCount = queryable.Count(),
                PageIndex = query.PageIndex,
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
            queryable = queryable.Include(m => m.Creator).AsQueryable();
            return queryable;
        }

        public IEnumerable<T> GetByStatus(string status)
        {
            return _context.Set<T>().Where(s => s.IsDeleted == false && s.Status == status);
        }
    }
}
