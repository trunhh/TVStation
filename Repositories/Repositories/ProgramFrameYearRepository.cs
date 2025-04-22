using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class ProgramFrameYearRepository : GenericRepository<Event>, IEventRepository
    {
        public ProgramFrameYearRepository(AppDbContext context) : base(context) { }

        public EventListDTO<Event> GetAll(EventQuery query)
        {
            var queryable = _context.Set<Event>().Where(s => s.IsDeleted == false); ;
            if (!string.IsNullOrEmpty(query.Keyword)) queryable = queryable.Where(s => s.Title.Contains(query.Keyword));
            if (!string.IsNullOrEmpty(query.Sector)) queryable = queryable.Where(s => s.Sector == query.Sector);
            if (!string.IsNullOrEmpty(query.Status)) queryable = queryable.Where(s => s.Status == query.Status);
            queryable = queryable.Include(m => m.Creator).AsQueryable();
            return new EventListDTO<Event>
            {
                List = queryable,
                TotalCount = queryable.Count(),
                ApprovedCount = queryable.Where(p => p.Status == PlanStatus.Approved).Count(),
                InProgressCount = queryable.Where(p => p.Status == PlanStatus.InProgress).Count(),
                WaitingApprovalCount = queryable.Where(p => p.Status == PlanStatus.WaitingForApproval).Count()
            };
        }
    }
}
