using Microsoft.EntityFrameworkCore;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans.Productions;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Data.Request;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProductionPlanRepositories
{
    public class MediaProjectRepository :
        ProductionPlanRepository<MediaProject, MediaProjectQuery>, IMediaProjectRepository
    {
        public MediaProjectRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override IQueryable<MediaProject> GetQueriedData(MediaProjectQuery query)
        {
            return base.GetQueriedData(query).Where(s => s.Airdate.Year == query.Year);
        }
    }
}
