using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject;
using TVStation.Data.QueryObject.Plans;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories
{
    public abstract class ProgramFrameRepository<T,Q> : GenericRepository<T,Q>
        where T : class, IEntity, IProgramFrame where Q : class, IPagingQuery, IProgramFrameQuery
    {
        public ProgramFrameRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override IQueryable<T> GetQueriedData(Q query)
        {
            return base.GetQueriedData(query).Where(s => s.Year == query.Year);
        }
    }
}
