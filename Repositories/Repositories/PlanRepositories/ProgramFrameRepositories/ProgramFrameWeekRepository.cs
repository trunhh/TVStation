using TVStation.Data.Model;
using TVStation.Data.Model.Plans.ProgramFrames;
using TVStation.Data.QueryObject.Plans.ProgramFrames;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories
{
    public class ProgramFrameWeekRepository :
        ProgramFrameRepository<ProgramFrameWeek, ProgramFrameWeekQuery>, IProgramFrameWeekRepository
    {
        public ProgramFrameWeekRepository(AppDbContext context) : base(context) { }
        protected override IQueryable<ProgramFrameWeek> GetQueriedData(ProgramFrameWeekQuery query)
        {
            return base.GetQueriedData(query).Where(s => s.Week == query.Week);
        }
    }
}
