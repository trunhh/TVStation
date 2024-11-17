using TVStation.Data.Model;
using TVStation.Data.Model.Plans.ProgramFrames;
using TVStation.Data.QueryObject.Plans.ProgramFrames;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories
{
    public class ProgramFrameBroadcastRepository :
        ProgramFrameRepository<ProgramFrameBroadcast, ProgramFrameBroadcastQuery>, IProgramFrameBroadcastRepository
    {
        public ProgramFrameBroadcastRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override IQueryable<ProgramFrameBroadcast> GetQueriedData(ProgramFrameBroadcastQuery query)
        {
            return base.GetQueriedData(query)
                .Where(s => s.Airdate > query.StartDate && s.Airdate < query.EndDate);
        }
    }
}
