using TVStation.Data.Model.Plans.ProgramFrames;
using TVStation.Data.QueryObject.Plans.ProgramFrames;

namespace TVStation.Repositories.IRepositories
{
    public interface IProgramFrameWeekRepository 
        : IPlanRepository<ProgramFrameWeek, ProgramFrameWeekQuery>
    {
    }
}
