using TVStation.Data.Model;
using TVStation.Data.Model.Plans.ProgramFrames;
using TVStation.Data.QueryObject.Plans.ProgramFrames;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories
{
    public class ProgramFrameYearRepository :
        ProgramFrameRepository<ProgramFrameYear, ProgramFrameYearQuery>, IProgramFrameYearRepository
    {
        public ProgramFrameYearRepository(AppDbContext context) : base(context) { }
    }
}
