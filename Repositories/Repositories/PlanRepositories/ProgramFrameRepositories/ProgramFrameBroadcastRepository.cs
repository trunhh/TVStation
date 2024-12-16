using System.Linq;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans.ProgramFrames;
using TVStation.Data.QueryObject.Plans.ProgramFrames;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories
{
    public class ProgramFrameBroadcastRepository :
        PlanRepository<ProgramFrameBroadcast, ProgramFrameBroadcastQuery>, IProgramFrameBroadcastRepository
    {
        public ProgramFrameBroadcastRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override IQueryable<ProgramFrameBroadcast> GetQueriedData(ProgramFrameBroadcastQuery query)
        {
            var queryable = base.GetQueriedData(query);
            if (query.StartDate != null)
            {
                queryable = queryable.Where(s => s.Airdate > query.StartDate);
            }
            if (query.EndDate != null)
            {
                queryable = queryable.Where(s => s.Airdate < query.EndDate);
            }
            return queryable;
        }
    }
}
