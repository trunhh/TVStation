using Microsoft.EntityFrameworkCore;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans.Productions;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProductionPlanRepositories
{
    public class ScriptProgramRepository :
        ProductionPlanRepository<ScriptProgram, ScriptProgramQuery>, IScriptProgramRepository
    {
        public ScriptProgramRepository(AppDbContext context) : base(context) { }
    }
}
