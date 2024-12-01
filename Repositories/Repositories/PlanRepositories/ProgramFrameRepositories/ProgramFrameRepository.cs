using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject;
using TVStation.Data.QueryObject.Plans;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProgramFrameRepositories
{
    public abstract class ProgramFrameRepository<T,Q> : PlanRepository<T,Q>
        where T : class, IEntity, IProgramFrame where Q : class, IPagingQuery, IProgramFrameQuery
    {
        public ProgramFrameRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override IQueryable<T> GetQueriedData(Q query)
        {
            var queryable = base.GetQueriedData(query);
            if (query.Year != null) queryable = queryable.Where(s => s.Year == query.Year);
            return queryable;
        }
    }
}
