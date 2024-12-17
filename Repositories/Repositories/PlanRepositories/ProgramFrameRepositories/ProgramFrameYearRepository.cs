using TVStation.Data.Model;
using TVStation.Data.Model.Plans.ProgramFrames;
using TVStation.Data.QueryObject.Plans.ProgramFrames;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories
{
    public class ProgramFrameYearRepository :
        PlanRepository<ProgramFrameYear, ProgramFrameYearQuery>, IProgramFrameYearRepository
    {
        public ProgramFrameYearRepository(AppDbContext context) : base(context) { }

        protected override IQueryable<ProgramFrameYear> GetQueriedData(ProgramFrameYearQuery query)
        {
            var queryable = base.GetQueriedData(query);
            if (query.Year != null) queryable = queryable.Where(s => s.Year == query.Year);
            return queryable;
        }
    }
}
