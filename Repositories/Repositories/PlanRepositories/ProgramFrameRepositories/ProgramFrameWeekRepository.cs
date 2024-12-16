using TVStation.Data.Model;
using TVStation.Data.Model.Plans.ProgramFrames;
using TVStation.Data.QueryObject.Plans.ProgramFrames;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories
{
    public class ProgramFrameWeekRepository :
        PlanRepository<ProgramFrameWeek, ProgramFrameWeekQuery>, IProgramFrameWeekRepository
    {
        public ProgramFrameWeekRepository(AppDbContext context) : base(context) { }
        protected override IQueryable<ProgramFrameWeek> GetQueriedData(ProgramFrameWeekQuery query)
        {
            var queryable = base.GetQueriedData(query);
            if (query.Airdate != null) queryable = queryable.Where(s => s.Airdate.Date == query.Airdate.Value.Date);
            return queryable;
        }
    }
}
