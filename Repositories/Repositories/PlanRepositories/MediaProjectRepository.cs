using Microsoft.EntityFrameworkCore;
using System.Linq;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Data.DTO;
using TVStation.Repositories.IRepositories;
using TVStation.Repositories.Repositories.PlanRepositories.ProductionPlanRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories
{
    public class MediaProjectRepository :
        PlanRepository<MediaProject, MediaProjectQuery>, IMediaProjectRepository
    {
        public MediaProjectRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override IQueryable<MediaProject> GetQueriedData(MediaProjectQuery query)
        {
            var queryable = base.GetQueriedData(query);
            if (query.StartDate != null)
            {
                queryable = queryable.Where(s => s.CreatedDate > query.StartDate);
            }
            if (query.EndDate != null)
            {
                queryable = queryable.Where(s => s.CreatedDate < query.EndDate);
            }
            if (query.SiteMapId != null && query.SiteMapId != Guid.Empty)
            {
                queryable = queryable
                    .Include(s => s.SiteMap)
                    .AsQueryable()
                    .Where(s => s.SiteMap != null && s.SiteMap.Id == query.SiteMapId);
            }
            queryable = queryable.Include(m => m.Creator).AsQueryable();
            return queryable;
        }
    }
}
