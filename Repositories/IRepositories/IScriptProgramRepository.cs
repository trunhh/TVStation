using TVStation.Data.Model.Plans.Productions;
using TVStation.Data.QueryObject.Plans.Productions;

namespace TVStation.Repositories.IRepositories
{
    public interface IScriptProgramRepository 
        : IPlanRepository<ScriptProgram, ScriptProgramQuery>
    {
    }
}
